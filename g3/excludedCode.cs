from class mob:

public void addIconControl(string imgURL, level Level) //adds icon to form
{
    btn.Image = System.Drawing.Image.FromFile(imgURL);
    btn.Click += new EventHandler(icon_Click);
    btn.Left = -Level.screenOffsetX + (int)xPos;
    btn.Top = -Level.screenOffsetY + (int)yPos;
    btn.Width = 18; //MAGIC NUMBER, TODO - set to be dependent on image
    btn.Height = 18;
    Level.parentForm.Controls.Add(btn); //TODO fix to remove activeForm and stop errors when debugging
}
private delegate void AddIconDelegate(string imgURL, level Level);

private void icon_Click(object sender, System.EventArgs e)
{
    Console.WriteLine("Icon clicked");
    //System.Windows.Forms.PictureBox pb = (System.Windows.Forms.PictureBox)sender;
    //pb.Visible = false;
    die();
    
}