using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Point = System.Drawing.Point;

using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Drawing;

// 공용함수
namespace PascalVocIntegrator
{
    static class CommonUtil
    {

        // 새로운 확장자로 바꾸는 함수
        public static string ConvertFullFileExtension(string fullFileName, string newExtension)
        {
            string directory = Path.GetDirectoryName(fullFileName);
            string fileExtension = Path.GetExtension(fullFileName);
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fullFileName);
            string newFullFilename = directory + "\\" + fileNameWithoutExtension + newExtension;

            return newFullFilename;
        }
        
        // 현재 파일명에 추가 이름 붙이는 함수
        public static string AddTailToFullFileName(string fullFileName, string newTail)
        {
            string directory = Path.GetDirectoryName(fullFileName);
            string fileExtension = Path.GetExtension(fullFileName);
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fullFileName);
            string newFullFilename = directory + "\\" + fileNameWithoutExtension + newTail + fileExtension;

            return newFullFilename;
        }
        
        // PascalVoc Format으로 저장
        public static void SaveAsIAEFormatFromPascalVocFormat(string fullFileName, ref PascalVocFormat pascalVocData)
        {
            // xml 정보 생성
            XmlDocument xmlDoc = new XmlDocument();

            // node 정의
            XmlNode rootNode = xmlDoc.CreateElement("annotation");
            xmlDoc.AppendChild(rootNode);

            XmlNode basicDrawingInformationNpde = xmlDoc.CreateElement("basic_drawing_information");
            rootNode.AppendChild(basicDrawingInformationNpde);

            XmlNode folderNode = xmlDoc.CreateElement("folder");
            basicDrawingInformationNpde.AppendChild(folderNode);

            XmlNode filenameNode = xmlDoc.CreateElement("filename");
            basicDrawingInformationNpde.AppendChild(filenameNode);

            XmlNode pathNode = xmlDoc.CreateElement("path");
            basicDrawingInformationNpde.AppendChild(pathNode);

            XmlNode sizeNode = xmlDoc.CreateElement("size");
            basicDrawingInformationNpde.AppendChild(sizeNode);

            XmlNode widthNode = xmlDoc.CreateElement("width");
            sizeNode.AppendChild(widthNode);

            XmlNode heightNode = xmlDoc.CreateElement("height");
            sizeNode.AppendChild(heightNode);

            XmlNode depthNode = xmlDoc.CreateElement("depth");
            sizeNode.AppendChild(depthNode);

            XmlNode externalBorderLineNode = xmlDoc.CreateElement("external_border_line");
            XmlNode externalBorderLineBndBoxNode = xmlDoc.CreateElement("bndbox");
            XmlNode externalBorderLineBndBoxXminNode = xmlDoc.CreateElement("xmin");
            XmlNode externalBorderLineBndBoxYminNode = xmlDoc.CreateElement("ymin");
            XmlNode externalBorderLineBndBoxXmaxNode = xmlDoc.CreateElement("xmax");
            XmlNode externalBorderLineBndBoxYmaxNode = xmlDoc.CreateElement("ymax");
            basicDrawingInformationNpde.AppendChild(externalBorderLineNode);
            externalBorderLineNode.AppendChild(externalBorderLineBndBoxNode);
            externalBorderLineBndBoxNode.AppendChild(externalBorderLineBndBoxXminNode);
            externalBorderLineBndBoxNode.AppendChild(externalBorderLineBndBoxYminNode);
            externalBorderLineBndBoxNode.AppendChild(externalBorderLineBndBoxXmaxNode);
            externalBorderLineBndBoxNode.AppendChild(externalBorderLineBndBoxYmaxNode);

            XmlNode pureDrawingAreaNode = xmlDoc.CreateElement("pure_drawing_area");
            XmlNode pureDrawingAreaNodeBndBoxNode = xmlDoc.CreateElement("bndbox");
            XmlNode pureDrawingAreaNodeBndBoxXminNode = xmlDoc.CreateElement("xmin");
            XmlNode pureDrawingAreaNodeBndBoxYminNode = xmlDoc.CreateElement("ymin");
            XmlNode pureDrawingAreaNodeBndBoxXmaxNode = xmlDoc.CreateElement("xmax");
            XmlNode pureDrawingAreaNodeBndBoxYmaxNode = xmlDoc.CreateElement("ymax");
            basicDrawingInformationNpde.AppendChild(pureDrawingAreaNode);
            pureDrawingAreaNode.AppendChild(pureDrawingAreaNodeBndBoxNode);
            pureDrawingAreaNodeBndBoxNode.AppendChild(pureDrawingAreaNodeBndBoxXminNode);
            pureDrawingAreaNodeBndBoxNode.AppendChild(pureDrawingAreaNodeBndBoxYminNode);
            pureDrawingAreaNodeBndBoxNode.AppendChild(pureDrawingAreaNodeBndBoxXmaxNode);
            pureDrawingAreaNodeBndBoxNode.AppendChild(pureDrawingAreaNodeBndBoxYmaxNode);

            XmlNode noteAreaNode = xmlDoc.CreateElement("note_area");
            XmlNode noteAreaNodeBndBoxNode = xmlDoc.CreateElement("bndbox");
            XmlNode noteAreaNodeBndBoxXminNode = xmlDoc.CreateElement("xmin");
            XmlNode noteAreaNodeBndBoxYminNode = xmlDoc.CreateElement("ymin");
            XmlNode noteAreaNodeBndBoxXmaxNode = xmlDoc.CreateElement("xmax");
            XmlNode noteAreaNodeBndBoxYmaxNode = xmlDoc.CreateElement("ymax");
            basicDrawingInformationNpde.AppendChild(noteAreaNode);
            noteAreaNode.AppendChild(noteAreaNodeBndBoxNode);
            noteAreaNodeBndBoxNode.AppendChild(noteAreaNodeBndBoxXminNode);
            noteAreaNodeBndBoxNode.AppendChild(noteAreaNodeBndBoxYminNode);
            noteAreaNodeBndBoxNode.AppendChild(noteAreaNodeBndBoxXmaxNode);
            noteAreaNodeBndBoxNode.AppendChild(noteAreaNodeBndBoxYmaxNode);

            XmlNode titleAreaNode = xmlDoc.CreateElement("title_area");
            XmlNode titleAreaNodeBndBoxNode = xmlDoc.CreateElement("bndbox");
            XmlNode titleAreaNodeBndBoxXminNode = xmlDoc.CreateElement("xmin");
            XmlNode titleAreaNodeBndBoxYminNode = xmlDoc.CreateElement("ymin");
            XmlNode titleAreaNodeBndBoxXmaxNode = xmlDoc.CreateElement("xmax");
            XmlNode titleAreaNodeBndBoxYmaxNode = xmlDoc.CreateElement("ymax");
            basicDrawingInformationNpde.AppendChild(titleAreaNode);
            titleAreaNode.AppendChild(titleAreaNodeBndBoxNode);
            titleAreaNodeBndBoxNode.AppendChild(titleAreaNodeBndBoxXminNode);
            titleAreaNodeBndBoxNode.AppendChild(titleAreaNodeBndBoxYminNode);
            titleAreaNodeBndBoxNode.AppendChild(titleAreaNodeBndBoxXmaxNode);
            titleAreaNodeBndBoxNode.AppendChild(titleAreaNodeBndBoxYmaxNode);

            XmlNode drawingAreaSeperatorNode = xmlDoc.CreateElement("drawing_area_separator");
            XmlNode drawingAreaSeperatorEdgeNode = xmlDoc.CreateElement("edge");
            XmlNode drawingAreaSeperatorEdgeXstartNode = xmlDoc.CreateElement("xstart");
            XmlNode drawingAreaSeperatorEdgeYstartNode = xmlDoc.CreateElement("ystart");
            XmlNode drawingAreaSeperatorEdgeXendNode = xmlDoc.CreateElement("xend");
            XmlNode drawingAreaSeperatorEdgeYendNode = xmlDoc.CreateElement("yend");
            basicDrawingInformationNpde.AppendChild(drawingAreaSeperatorNode);
            drawingAreaSeperatorNode.AppendChild(drawingAreaSeperatorEdgeNode);
            drawingAreaSeperatorEdgeNode.AppendChild(drawingAreaSeperatorEdgeXstartNode);
            drawingAreaSeperatorEdgeNode.AppendChild(drawingAreaSeperatorEdgeYstartNode);
            drawingAreaSeperatorEdgeNode.AppendChild(drawingAreaSeperatorEdgeXendNode);
            drawingAreaSeperatorEdgeNode.AppendChild(drawingAreaSeperatorEdgeYendNode);

            // 내용 설정
            folderNode.InnerText = pascalVocData.folder;
            filenameNode.InnerText = pascalVocData.filename;
            pathNode.InnerText = pascalVocData.path;

            widthNode.InnerText = pascalVocData.width.ToString();
            heightNode.InnerText = pascalVocData.height.ToString();
            depthNode.InnerText = pascalVocData.depth.ToString();

            externalBorderLineBndBoxXminNode.InnerText = 0.ToString();
            externalBorderLineBndBoxYminNode.InnerText = 0.ToString();
            externalBorderLineBndBoxXmaxNode.InnerText = 0.ToString();
            externalBorderLineBndBoxYmaxNode.InnerText = 0.ToString();

            pureDrawingAreaNodeBndBoxXminNode.InnerText = 0.ToString();
            pureDrawingAreaNodeBndBoxYminNode.InnerText = 0.ToString();
            pureDrawingAreaNodeBndBoxXmaxNode.InnerText = 0.ToString();
            pureDrawingAreaNodeBndBoxYmaxNode.InnerText = 0.ToString();

            noteAreaNodeBndBoxXminNode.InnerText = 0.ToString();
            noteAreaNodeBndBoxYminNode.InnerText = 0.ToString();
            noteAreaNodeBndBoxXmaxNode.InnerText = 0.ToString();
            noteAreaNodeBndBoxYmaxNode.InnerText = 0.ToString();

            titleAreaNodeBndBoxXminNode.InnerText = 0.ToString();
            titleAreaNodeBndBoxYminNode.InnerText = 0.ToString();
            titleAreaNodeBndBoxXmaxNode.InnerText = 0.ToString();
            titleAreaNodeBndBoxYmaxNode.InnerText = 0.ToString();

            drawingAreaSeperatorEdgeXstartNode.InnerText = 0.ToString();
            drawingAreaSeperatorEdgeYstartNode.InnerText = 0.ToString();
            drawingAreaSeperatorEdgeXendNode.InnerText = 0.ToString();
            drawingAreaSeperatorEdgeYendNode.InnerText = 0.ToString();

            // object 가져오기
            // pascal voc에는 symbol, line의 구분과 type의 구분이 없으므로, 
            // symbol object의 unspecified type으로 가져올 것
            int objectCount = pascalVocData.objectList.Count;
            for (int i = 0; i < objectCount; i++)
            {
                XmlNode objectNode = xmlDoc.CreateElement("symbol_object");
                XmlNode objectTypeNode = xmlDoc.CreateElement("type");
                XmlNode objectClassNode = xmlDoc.CreateElement("class");
                XmlNode objectBndboxNode = xmlDoc.CreateElement("bndbox");
                XmlNode objectBndboxXminNode = xmlDoc.CreateElement("xmin");
                XmlNode objectBndboxYminNode = xmlDoc.CreateElement("ymin");
                XmlNode objectBndboxXmaxNode = xmlDoc.CreateElement("xmax");
                XmlNode objectBndboxYmaxNode = xmlDoc.CreateElement("ymax");
                XmlNode objectDegreeNode = xmlDoc.CreateElement("degree");
                XmlNode objectFlipNode = xmlDoc.CreateElement("flip");

                rootNode.AppendChild(objectNode);
                objectNode.AppendChild(objectTypeNode);
                objectNode.AppendChild(objectClassNode);
                objectNode.AppendChild(objectBndboxNode);
                objectBndboxNode.AppendChild(objectBndboxXminNode);
                objectBndboxNode.AppendChild(objectBndboxYminNode);
                objectBndboxNode.AppendChild(objectBndboxXmaxNode);
                objectBndboxNode.AppendChild(objectBndboxYmaxNode);
                objectNode.AppendChild(objectDegreeNode);

                // node node 내용 정의
                objectTypeNode.InnerText = "unspecified_symbol";
                objectClassNode.InnerText = pascalVocData.objectList[i].name;
                objectBndboxXminNode.InnerText = pascalVocData.objectList[i].xMin.ToString();
                objectBndboxYminNode.InnerText = pascalVocData.objectList[i].yMin.ToString();
                objectBndboxXmaxNode.InnerText = pascalVocData.objectList[i].xMax.ToString();
                objectBndboxYmaxNode.InnerText = pascalVocData.objectList[i].yMax.ToString();
                objectDegreeNode.InnerText = 0.ToString();
                objectFlipNode.InnerText = "n";
            }

            // 파일명으로 저장
            xmlDoc.Save(fullFileName);
        }
        
        // pascal voc format 파일 읽기
        public static PascalVocFormat ReadPascalVocFormat(string fullFilename)
        {
            // 메모리에 저장            
            PascalVocFormat data = new PascalVocFormat();

            // 문서 정의
            XmlDocument xmlDoc = new XmlDocument();

            // 문서 부르기
            xmlDoc.Load(fullFilename);

            // 일반 node 선택 및 정의
            XmlNodeList xmlAnnotationNodes = xmlDoc.SelectNodes("/annotation");
            XmlNode xmlAnnotationNode = xmlAnnotationNodes.Item(0);

            // 일반 node 정보 가져오기
            data.folder = xmlAnnotationNode.SelectSingleNode("folder").InnerText;
            data.filename = xmlAnnotationNode.SelectSingleNode("filename").InnerText;
            data.path = xmlAnnotationNode.SelectSingleNode("path").InnerText;
            data.database = xmlAnnotationNode.SelectSingleNode("source").SelectSingleNode("database").InnerText;
            data.width = Convert.ToInt32(xmlAnnotationNode.SelectSingleNode("size").SelectSingleNode("width").InnerText);
            data.height = Convert.ToInt32(xmlAnnotationNode.SelectSingleNode("size").SelectSingleNode("height").InnerText);
            data.depth = Convert.ToInt32(xmlAnnotationNode.SelectSingleNode("size").SelectSingleNode("depth").InnerText);
            data.segmented = xmlAnnotationNode.SelectSingleNode("segmented").InnerText;

            // object node 선택 및 정의
            XmlNodeList xmlObjectNodes = xmlDoc.SelectNodes("/annotation/object");

            // 메모리에 저장

            List<PascalVocObject> pascalVocObjectList = new List<PascalVocObject>();

            // object node 갯수만큼 돌면서 정보 가져오기
            foreach (XmlNode xmlObjectNode in xmlObjectNodes)
            {
                PascalVocObject objectData = new PascalVocObject();

                // xml 파일에 노드가 비어있으면 발생하는 에러 처리
                objectData.name = xmlObjectNode.SelectSingleNode("name").InnerText;
                objectData.pose = xmlObjectNode.SelectSingleNode("pose").InnerText;

                XmlNode truncatedNode = xmlObjectNode.SelectSingleNode("truncated");
                if (truncatedNode != null) objectData.truncated = Convert.ToInt32(truncatedNode.InnerText);

                XmlNode difficultNode = xmlObjectNode.SelectSingleNode("difficult");
                if (difficultNode != null) objectData.truncated = Convert.ToInt32(difficultNode.InnerText);

                XmlNode occludedNode = xmlObjectNode.SelectSingleNode("truncated");
                if (occludedNode != null) objectData.truncated = Convert.ToInt32(occludedNode.InnerText);

                // 바운딩 박스 선택
                XmlNode xmlObjectBndboxNode = xmlObjectNode.SelectSingleNode("bndbox");
                objectData.xMin = Convert.ToInt32(xmlObjectBndboxNode.SelectSingleNode("xmin").InnerText);
                objectData.yMin = Convert.ToInt32(xmlObjectBndboxNode.SelectSingleNode("ymin").InnerText);
                objectData.xMax = Convert.ToInt32(xmlObjectBndboxNode.SelectSingleNode("xmax").InnerText);
                objectData.yMax = Convert.ToInt32(xmlObjectBndboxNode.SelectSingleNode("ymax").InnerText);

                pascalVocObjectList.Add(objectData);
            }

            data.objectList = pascalVocObjectList;

            return data;
        }        
    }    
}