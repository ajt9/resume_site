namespace MyBlazorApp1.Components
{
    public class SkillTablet
    {
        public string label { get; set; }

        public int level { get; set; }

        public SkillTablet(string lbl, int lvl)
        {
            label = lbl;
            level = lvl;
        }
    }
}
