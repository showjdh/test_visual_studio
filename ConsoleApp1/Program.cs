using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExifLibrary;

//test

namespace ConsoleApp1
{
    class Program
    {
        public static string[] files;

        static void Main(string[] args)
        {
            string folderName = @"D:\test2";
            string fileName = "";
            string fileNameFull = "";
            string dstFolderName="";
            string patternIPhoneJPG = "^P\\d{8}_\\d{6}";
            string patternGalaxyJPG = "^\\d{4}-\\d{2}-\\d{2}-\\d{2}-\\d{2}-\\d{2}";
            string patternIPhoneMOV = ".mov$";
            string patternGalaxyMP4 = ".mp4$";
            int jpgCount=0;
            int videoCount = 0;
            int errorCount = 0;

            ExifFile fileExif;
            DateTime dateTime;

            if (System.IO.Directory.Exists(folderName))
            {
                files = System.IO.Directory.GetFiles(folderName);
                foreach (string s in files)
                {
                    //Console.WriteLine(s);
                    fileNameFull = s;
                    fileName = System.IO.Path.GetFileName(s);

                    fileExif = ExifFile.Read(fileNameFull);
                    
                    //fileName = System.IO.Path.Combine(folderName, s);
                    if (System.Text.RegularExpressions.Regex.IsMatch(fileName, patternIPhoneMOV))
                    {
     
                        dstFolderName = folderName +"\\"+ fileName.Substring(1, 8);
                        //Console.WriteLine(dstFolderName);
                        if (System.IO.Directory.Exists(dstFolderName))
                        {
                            System.IO.Directory.Delete(dstFolderName, true);
                        }
                        System.IO.Directory.CreateDirectory(dstFolderName);
                        System.IO.File.Copy(fileNameFull, dstFolderName +"\\" +fileName);
                        Console.WriteLine("IPhone MOV : " + fileName + " -> " + dstFolderName);
                        videoCount++;
                    }

                    else if (System.Text.RegularExpressions.Regex.IsMatch(fileName, patternGalaxyMP4))
                    {
                        dstFolderName = folderName + "\\" + fileName.Substring(0, 8);
                        //Console.WriteLine(dstFolderName);
                        if (System.IO.Directory.Exists(dstFolderName))
                        {
                            System.IO.Directory.Delete(dstFolderName, true);
                        }
                        System.IO.Directory.CreateDirectory(dstFolderName);
                        System.IO.File.Copy(fileNameFull, dstFolderName + "\\" + fileName);
                        Console.WriteLine("Galaxy MP4 : " + fileName + " -> " + dstFolderName);
                        videoCount++;
                    }


        
                    else if (System.Text.RegularExpressions.Regex.IsMatch(fileName, patternIPhoneJPG))
                    {
                         //fileName = fileName.Substring(1, 8) +"-"+ fileName.Substring(10, 6);
                        //dateTime = fileName.Substring(7, 2) + "/" + fileName.Substring(5, 2) + "/"+fileName.Substring(1, 4)+" "+ fileName.Substring(10, 2)+":" + fileName.Substring(12, 2) + ":" + fileName.Substring(14, 2)+ " AM";
                        Console.WriteLine(fileName);
                        int year = Convert.ToInt32(fileName.Substring(1, 4));
                        int month = Convert.ToInt32(fileName.Substring(5, 2));
                        int day = Convert.ToInt32(fileName.Substring(7, 2));
                        int hour = Convert.ToInt32(fileName.Substring(10, 2));
                        int minute = Convert.ToInt32(fileName.Substring(12, 2));
                        int second = Convert.ToInt32(fileName.Substring(14, 2));
                        //Console.WriteLine(year);
                        //Console.WriteLine(month);
                        //Console.WriteLine(day);
                        //Console.WriteLine(hour);
                        //Console.WriteLine(minute);
                        //Console.WriteLine(second);
                        //Console.WriteLine(year.ToString+month.ToString+day+hour+minute+second);

                        dateTime = new DateTime(year,month,day,hour,minute,second);
                        //Console.WriteLine("IPhone JPG : " + dateTime);
                        //fileExif.Properties[ExifTag.DateTimeDigitized].Value = dateTime;
                        fileExif.Properties[ExifTag.DateTimeOriginal].Value = dateTime;
                        if (System.IO.Directory.Exists(folderName + @"\temp")==false)
                            System.IO.Directory.CreateDirectory(folderName + @"\temp\");
                        fileExif.Save(folderName+@"\temp\"+fileName);
                        jpgCount++;

                    }

                    else if (System.Text.RegularExpressions.Regex.IsMatch(fileName, patternGalaxyJPG))
                    {
                        //dateTime = fileName.Substring(0, 4) + fileName.Substring(5, 2) + fileName.Substring(8, 2) +"-"+ fileName.Substring(11, 2) + fileName.Substring(14, 2) + fileName.Substring(17, 2);
                        Console.WriteLine(fileName);
                        int year = Convert.ToInt32(fileName.Substring(0, 4));
                        int month = Convert.ToInt32(fileName.Substring(5, 2));
                        int day = Convert.ToInt32(fileName.Substring(8, 2));
                        int hour = Convert.ToInt32(fileName.Substring(11, 2));
                        int minute = Convert.ToInt32(fileName.Substring(14, 2));
                        int second = Convert.ToInt32(fileName.Substring(17, 2));
                        //Console.WriteLine(year);
                        //Console.WriteLine(month);
                        //Console.WriteLine(day);
                        //Console.WriteLine(hour);
                        //Console.WriteLine(minute);
                        //Console.WriteLine(second);
                        //Console.WriteLine(year.ToString+month.ToString+day+hour+minute+second);

                        dateTime = new DateTime(year, month, day, hour, minute, second);
                        //Console.WriteLine("Galaxy JPG : " + dateTime);
                        fileExif.Properties[ExifTag.DateTimeDigitized].Value = dateTime;
                        fileExif.Properties[ExifTag.DateTimeOriginal].Value = dateTime;
                        if (System.IO.Directory.Exists(folderName + @"\temp") == false)
                            System.IO.Directory.CreateDirectory(folderName + @"\temp\");
                        fileExif.Save(folderName + @"\temp\" + fileName);
                        jpgCount++;
                    }

                    else
                    {
                        Console.WriteLine("Nothing matched!!! = " + fileName);
                        errorCount++;
                    }


                    //ExifFile file = ExifFile.Read(fileName);
                    //file.Properties[ExifTag.DateTimeOriginal].Value = new DateTime(2017, 01, 02, 03, 04, 05, 06);
                    //file.Save(fileName);
                    //Console.WriteLine(fileName);

                    //fileCount++;
                }
                Console.WriteLine("jpgCount = "+ jpgCount);
                Console.WriteLine("videoCount = "+ videoCount);
                Console.WriteLine("totalCount = "+ (jpgCount+videoCount+errorCount));
            }

        }
    }
}
