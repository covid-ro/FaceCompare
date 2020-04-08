using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    //Upload
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (FileUpload1.HasFile)
        {
            Label3.Text = "Face Compare -";
            Proxy.ServiceClient client = new Proxy.ServiceClient();
            string folderPath = Server.MapPath("~/Files/");

            //Check whether Directory (Folder) exists.
            if (!Directory.Exists(folderPath))
            {
                //If Directory (Folder) does not exists Create it.
                Directory.CreateDirectory(folderPath);
            }

            FileUpload1.SaveAs(folderPath + Path.GetFileName(FileUpload1.FileName));


            FileUpload1.PostedFile.InputStream.Seek(0, SeekOrigin.Begin);
            using (var binaryReader = new BinaryReader(FileUpload1.PostedFile.InputStream))
            {
                byte[] fileBytes = binaryReader.ReadBytes(FileUpload1.PostedFile.ContentLength);
                bool compareBase = chkCompareBase.Checked;
                var fileName = client.UploadBytes(fileBytes, compareBase);
                if (compareBase)
                {
                    Label3.Text = "Face Compare - Imaginea de baza a fost incarcata pe server.";
                    Image2.ImageUrl = "~/Files/" + Path.GetFileName(FileUpload1.FileName);
                }                                
                else
                {
                    Label3.Text = "Face Compare - " + fileName;
                    Image1.ImageUrl = "~/Files/" + Path.GetFileName(FileUpload1.FileName);
                }
                Session["file"] = fileName;
                chkCompareBase.Checked = false;
            }
        }
        //var fileName = client.Upload(FileUpload1.FileContent);
        //Session["file"] = fileName;
    }
    //Download
    protected void Button2_Click(object sender, EventArgs e)
    {
        Proxy.ServiceClient client = new Proxy.ServiceClient();
        var stream = client.Download(Session["file"].ToString());
        StreamReader reader = new StreamReader(stream);
        Response.Write(reader.ReadToEnd());
    }
}