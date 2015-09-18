#region << 版 本 注 释 >>
/*
 * ======================================================
 * Copyright(c) 2012-2015 Zeal Fung, All Rights Reserved.
 * ======================================================
 *  
 * 【当前类文件的功能】
 *  
 *  
 * 作者：[冯梓易]   时间：2014/2/1 星期六 下午 01:40:40
 * 文件名：DataMigration
 * 版本：V1.0.0
 * 
 * 修改者：           时间：               
 * 修改说明：
 * ======================================================
*/
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using DesktopHelper.Entity;
using DesktopHelper.DataAccess;
using Andwho.Logger;

namespace DesktopHelper.Util
{
    public class DataMigration
    {
        private DataMigrationData dmData = new DataMigrationData();
        Log log = new Log(true);

        /// <summary>
        /// 开始更新网址数据
        /// </summary>
        public void LoadWebsiteXML()
        {
            try
            {
                string path = Application.StartupPath + @"\XML\WebsiteList.xml";
                if (!File.Exists(path)) return;
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(path);
                XmlNodeList tabNodes = xmlDoc.SelectNodes("WebsiteList/TabType");
                if (tabNodes != null && tabNodes.Count > 0)
                {
                    //清空网址表
                    dmData.ClearWebsiteData();
                    string type = string.Empty;
                    Website website = null;
                    foreach (XmlNode node in tabNodes)
                    {
                        type = node.Attributes[1].Value;
                        foreach (XmlNode cNode in node.ChildNodes)
                        {
                            website = new Website();
                            website.Name = cNode.Attributes[0].Value;
                            website.Type = type;
                            website.Index = cNode.Attributes[1].Value;
                            website.Url = cNode.Attributes[2].Value;
                            website.IconPath = cNode.Attributes[3].Value;
                            //插入数据库
                            dmData.InsertWebsiteData(website);
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                log.WriteLog(ex.ToString());
            }
        }

        /// <summary>
        /// 开始更新城市数据
        /// </summary>
        public void LoadCityXML()
        {
            try
            {
                string path = Application.StartupPath + @"\XML\CityList.xml";
                if (!File.Exists(path)) return;
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(path);
                XmlNodeList cityNodes = xmlDoc.SelectNodes("CityList/Sheng");
                if (cityNodes != null && cityNodes.Count > 0)
                {
                    //清空网址表
                    dmData.ClearCityData();
                    string shengName = string.Empty;
                    string shiName = string.Empty;
                    City city = null;
                    foreach (XmlNode shengNode in cityNodes)
                    {
                        //省级
                        city = new City();
                        shengName = shengNode.Attributes[0].Value;
                        city.Name = shengName;
                        dmData.InsertCityData(city);
                        foreach (XmlNode shiNode in shengNode.ChildNodes)
                        {
                            //市级
                            city = new City();
                            shiName = shiNode.Attributes[0].Value;
                            city.Name = shiName;
                            city.ShengParent = shengName;
                            dmData.InsertCityData(city);
                            foreach (XmlNode xianNode in shiNode.ChildNodes)
                            {
                                //县级
                                city = new City();
                                city.Name = xianNode.Attributes[0].Value;
                                city.ShengParent = shengName;
                                city.ShiParent = shiName;
                                city.Code = xianNode.InnerText;
                                dmData.InsertCityData(city);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.WriteLog(ex.ToString());
            }
        }
    }
}
