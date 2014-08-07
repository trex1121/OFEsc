using UnityEngine;
using System.Collections;
using System.Xml;
using System.Text;
using System.IO;
using System;

public class Instructions : MonoBehaviour
{
	//TODO setup a xml parse
	//TODO write to screen text for Build Changelog and input cmds/ player insturctions
	//TODO Display text to GUI bounding box
	//TODO append info from xml to Changlog.txt
	//TODO load main level
	private string xmlPath;
	private string changePath;

	public bool programmer;

		// Use this for initialization
		void Start ()
		{
			xmlPath = Application.dataPath+"/Scripts/Data"+"/Change.xml";
			changePath = "/Users/trex1121/Desktop/Office Game Builds/Change Logs.txt";
		}
	
		// Update is called once per frame
		void OnGUI ()
		{
			GUI.Box (new Rect(Screen.width/7f, 0, 720, 480), ReadXML().ToString());
			if(GUI.Button(new Rect(20,40,90,20), "Load Level 1")) 
			{
				Application.LoadLevel(1);
			}
			
			if(programmer)
			if(GUI.Button(new Rect(20,70,100,20), "Append to File"))
			{
				AppendChangeLog();
			}
			
		}

		StringBuilder ReadXML ()
		{
		//Parse XML to Full String Document

			StringBuilder text = new StringBuilder();

			XmlDocument file = new XmlDocument();

			if(File.Exists(xmlPath))
			{
				Debug.Log("<color=green>"+xmlPath+" found..... /n data being parsed"+ "</color>");
				file.Load(xmlPath);

				XmlNodeList build = file.GetElementsByTagName("log");

				foreach(XmlNode nodelisted in build) // add each node in order to string
				{
					text.Append(nodelisted.InnerText).Append("/n");

				}


		}

		else{Debug.LogError("[-] "+ xmlPath+" DOES NOT EXIST FOR PARSING");}

		return text;
		}

	void AppendChangeLog ()
	{
		StringBuilder text = new StringBuilder();
		
		XmlDocument file = new XmlDocument();
		
		if(File.Exists(changePath))
		{
			Debug.Log("<color=red> Bam No Fuctionionality </color>");
			file.Load(xmlPath);
			
			XmlNode build = file.GetElementById("build");
			XmlNode change = file.GetElementById("change");


		}
	}
}

