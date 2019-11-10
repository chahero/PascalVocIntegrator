using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;

namespace PascalVocIntegrator
{
    public partial class FormMain : Form
    {        
        public FormMain()
        {
            InitializeComponent();

            // listViewFiles 설정
            listViewFiles.View = View.Details;
            listViewFiles.GridLines = true;
            listViewFiles.FullRowSelect = true;
            listViewFiles.HeaderStyle = ColumnHeaderStyle.Clickable;
            listViewFiles.CheckBoxes = true;

            listViewFiles.Columns.Add("v", 40);
            listViewFiles.Columns.Add("file name", 150);
            listViewFiles.Columns.Add("directory", 200);
        }

        // 파일 이름이나 전체 파일 경로를 입력 받아서, _로 시작하는 tail을 반환
        private string GetFileNameTail(string fileName, string tail)
        {
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
            int tailPosition = fileNameWithoutExtension.LastIndexOf(tail);
            int fileNameLength = fileNameWithoutExtension.Length;

            string currentTail = string.Empty;
            if (tailPosition != -1)
                currentTail = fileNameWithoutExtension.Substring(tailPosition, fileNameLength - tailPosition);

            return currentTail;
        }

        private void listViewFiles_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])(e.Data.GetData(DataFormats.FileDrop, false));

            // listview files에 추가
            AddFilesIntoListView(files, ref listViewFiles);
        }

        private void listViewFiles_DragEnter(object sender, DragEventArgs e)
        {
            bool bDesignatedFileExist = false;

            // 여러 파일 리스트를 가져옴
            string[] files = (string[])(e.Data.GetData(DataFormats.FileDrop, false));

            // 여러 파일 중 하나라도 xml 파일이 있는지 확인
            foreach (string strFile in files)
            {
                string fileExtension = Path.GetExtension(strFile);
                if (fileExtension.ToLower() == ".xml")
                {
                    bDesignatedFileExist = true;
                }
            }

            // 여러 파일 중 하나라도 xml 파일이 있으면 copy 표시
            if (bDesignatedFileExist == true)
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }


        // File List를 지정한 ListView에 추가
        private void AddFilesIntoListView(string[] files, ref ListView listView)
        {
            // 가져온 파일들에 대하여 처리
            foreach (string strFile in files)
            {
                string fileExtension = Path.GetExtension(strFile);
                string strFileName = Path.GetFileName(strFile);
                string directoryName = Path.GetDirectoryName(strFile);

                ListViewItem lvItem = new ListViewItem();

                if (fileExtension.ToLower() == ".xml")
                {
                    lvItem.SubItems.Add(strFileName);
                    lvItem.SubItems.Add(directoryName);
                    listView.Items.Add(lvItem);
                }
                else
                {
                    continue;
                }
            }
        }

        private void listViewFiles_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                // 선택된 줄들의 모음 반환
                var varSelectedCollections = listViewFiles.SelectedIndices;

                for (int i = varSelectedCollections.Count - 1; i >= 0; i--)
                {
                    int iIndex = varSelectedCollections[i];
                    listViewFiles.Items[iIndex].Remove();
                }
            }
        }

        private void listViewFiles_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void buttonSaveAsIntegratedPascalVocFormat_Click(object sender, EventArgs e)
        {
            int iListCount = listViewFiles.Items.Count;

            if (iListCount < 2)
            {
                MessageBox.Show("통합할 파일을 2개 이상 추가해 주세요");
                return;
            }
            else
            {
                // 경고 메시지
                DialogResult ret = MessageBox.Show("Filename will be overwritten. If you need, you should make a copy.", "Filename Overwritten Warning", MessageBoxButtons.YesNo);

                if (ret == DialogResult.No)
                    return;
                                
                // 통합할 파일 여러개 돌면서, 리스트에 추가
                List<PascalVocFormat> pascalVocDataList = new List<PascalVocFormat>();

                for (int i = 0; i < iListCount; i++)
                {
                    // ListView에서 이름 가져와서, 가져올 파일명 만들기
                    string fileName = listViewFiles.Items[i].SubItems[1].Text;
                    string directoryName = listViewFiles.Items[i].SubItems[2].Text;
                    string fullFileName = directoryName + "\\" + fileName;

                    PascalVocFormat pascalVocData = new PascalVocFormat();

                    // 파일 있는지 확인
                    if (File.Exists(fullFileName) == false)
                        return;

                    // PascalVocl Format으로 읽어오기
                    pascalVocData = CommonUtil.ReadPascalVocFormat(fullFileName);
                    pascalVocDataList.Add(pascalVocData);                    
                }



                // 통합할 파일 여러개 돌면서, 파일 내용 통합
                // 일반적 정보는 첫번째 파일 정보로 대체
                // object 정보는 둘을 통합

                // ListView에서 이름 가져와서, 가져올 파일명 만들기
                string firstFilename = listViewFiles.Items[0].SubItems[1].Text;
                string firstDirectoryName = listViewFiles.Items[0].SubItems[2].Text;
                string firstFullFileName = firstDirectoryName + "\\" + firstFilename;
                string newFullFieName = CommonUtil.AddTailToFullFileName(firstFullFileName, "_integrated");

                // xml 정보 생성
                XmlDocument xmlDoc = new XmlDocument();

                // node 정의
                XmlNode rootNode = xmlDoc.CreateElement("annotation");
                XmlNode folderNode = xmlDoc.CreateElement("folder");
                XmlNode filenameNode = xmlDoc.CreateElement("filename");
                XmlNode pathNode = xmlDoc.CreateElement("path");
                XmlNode sourceNode = xmlDoc.CreateElement("source");
                XmlNode databaseNode = xmlDoc.CreateElement("database");
                XmlNode sizeNode = xmlDoc.CreateElement("size");
                XmlNode widthNode = xmlDoc.CreateElement("width");
                XmlNode heightNode = xmlDoc.CreateElement("height");
                XmlNode depthNode = xmlDoc.CreateElement("depth");
                XmlNode segmentedNode = xmlDoc.CreateElement("segmented");
                
                // 구조 정의
                xmlDoc.AppendChild(rootNode);
                rootNode.AppendChild(folderNode);
                rootNode.AppendChild(filenameNode);
                rootNode.AppendChild(pathNode);
                rootNode.AppendChild(sourceNode);
                sourceNode.AppendChild(databaseNode);
                rootNode.AppendChild(sizeNode);
                sizeNode.AppendChild(widthNode);
                sizeNode.AppendChild(heightNode);
                sizeNode.AppendChild(depthNode);
                rootNode.AppendChild(segmentedNode);
                
                for (int i = 0; i < pascalVocDataList.Count; i++)
                {
                    // 첫번째 파일에서 heder 정보 (folder, filename, path, source, size, segmented) 가져오기
                    // 첫번째 파일에서 object 정보 가져오기                    
                    if (i == 0)
                    {
                        folderNode.InnerText = pascalVocDataList[i].folder;
                        filenameNode.InnerText = pascalVocDataList[i].filename;
                        pathNode.InnerText = pascalVocDataList[i].path;
                        databaseNode.InnerText = "Unknown";
                        widthNode.InnerText = pascalVocDataList[i].width.ToString();
                        heightNode.InnerText = pascalVocDataList[i].height.ToString();
                        depthNode.InnerText = pascalVocDataList[i].depth.ToString();
                        segmentedNode.InnerText = 0.ToString();
                    }

                    // 모든 파일에서 object 정보만 가져와서 저장
                    // object                    
                    for (int j = 0; j < pascalVocDataList[i].objectList.Count; j++)
                    {
                        XmlNode objectNode = xmlDoc.CreateElement("object");
                        XmlNode objectNameNode = xmlDoc.CreateElement("name");
                        XmlNode objectPoseNode = xmlDoc.CreateElement("pose");
                        XmlNode objectTruncatedNode = xmlDoc.CreateElement("truncated");
                        XmlNode objectDifficultNode = xmlDoc.CreateElement("difficult");
                        XmlNode objectOccludedNode = xmlDoc.CreateElement("occluded");
                        XmlNode objectBndboxNode = xmlDoc.CreateElement("bndbox");
                        XmlNode objectBndboxXminNode = xmlDoc.CreateElement("xmin");
                        XmlNode objectBndboxYminNode = xmlDoc.CreateElement("ymin");
                        XmlNode objectBndboxXmaxNode = xmlDoc.CreateElement("xmax");
                        XmlNode objectBndboxYmaxNode = xmlDoc.CreateElement("ymax");

                        rootNode.AppendChild(objectNode);
                        objectNode.AppendChild(objectNameNode);
                        objectNode.AppendChild(objectPoseNode);
                        objectNode.AppendChild(objectTruncatedNode);
                        objectNode.AppendChild(objectDifficultNode);
                        objectNode.AppendChild(objectOccludedNode);
                        objectNode.AppendChild(objectBndboxNode);
                        objectBndboxNode.AppendChild(objectBndboxXminNode);
                        objectBndboxNode.AppendChild(objectBndboxYminNode);
                        objectBndboxNode.AppendChild(objectBndboxXmaxNode);
                        objectBndboxNode.AppendChild(objectBndboxYmaxNode);

                        // node node 내용 정의
                        objectNameNode.InnerText = pascalVocDataList[i].objectList[j].name;
                        objectPoseNode.InnerText = "Unspecified";
                        objectTruncatedNode.InnerText = "0";
                        objectDifficultNode.InnerText = "0";
                        objectOccludedNode.InnerText = "0";
                        objectBndboxXminNode.InnerText = pascalVocDataList[i].objectList[j].xMin.ToString();
                        objectBndboxYminNode.InnerText = pascalVocDataList[i].objectList[j].yMin.ToString();
                        objectBndboxXmaxNode.InnerText = pascalVocDataList[i].objectList[j].xMax.ToString();
                        objectBndboxYmaxNode.InnerText = pascalVocDataList[i].objectList[j].yMax.ToString();
                    }
                }

                // 파일명으로 저장
                xmlDoc.Save(newFullFieName);
            }

            MessageBox.Show("변환이 완료되었습니다.");
        }
    }
}
