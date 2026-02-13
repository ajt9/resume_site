namespace MyBlazorApp1.Components
{
    public class PointToggle
    {
        public string label { get; set; }
        public string content { get; set; }

        public string style { get; set; }
        public bool isHidden { get; set; }

        public PointToggle(string lbl, string cntnt)
        {
            label = lbl;
            content = cntnt;
            isHidden = true;
            style = "";
        }
        public void Toggle()
        {
            isHidden = !isHidden;
            style = isHidden ? "" : "border-left: 2px solid white;padding-left: 20px;";
        }
    }
}
