namespace MyBlazorApp1.Components
{
    public class PointToggle
    {
        public string label { get; set; }
        public string content { get; set; }
        public bool isHidden { get; set; }

        public PointToggle(string lbl, string cntnt)
        {
            label = lbl;
            content = cntnt;
            isHidden = true;
        }
        public void Toggle()
        {
            isHidden = !isHidden;
        }
    }
}
