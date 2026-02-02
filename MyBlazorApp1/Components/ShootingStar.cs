public class ShootingStar
{
    public enum EdgeOrigin
    {
        Left,
        Right,
        Top,
        Bottom
    }

    public enum GlowColor
    {
        Red,
        Orange,
        Yellow,
        White
    }

    public enum StarMagnitude
    {
        Small,
        Medium,
        Large
    }

    public int Id { get; set; }
    public double StartLeft { get; set; }
    public double StartTop { get; set; }
    public double EndLeft { get; set; }
    public double EndTop { get; set; }
    public double Duration { get; set; }
    public double Delay { get; set; }
    public int Size { get; set; }
    public double GlowSize { get; set; }
    public EdgeOrigin Origin { get; set; }
    public GlowColor Color { get; set; }
    public StarMagnitude Magnitude { get; set; }

    private static readonly Dictionary<GlowColor, (string rgb, string glowRgb)> ColorMap = new()
    {
        { GlowColor.Red, ("255, 0, 0", "255, 50, 50") },
        { GlowColor.Orange, ("255, 165, 0", "255, 200, 100") },
        { GlowColor.Yellow, ("255, 255, 0", "255, 255, 150") },
        { GlowColor.White, ("255, 255, 255", "255, 255, 255") }
    };

    public ShootingStar(int id, Random random)
    {
        ArgumentNullException.ThrowIfNull(random);

        Id = id;
        Origin = (EdgeOrigin)random.Next(0, 4);
        Color = (GlowColor)random.Next(0, ColorMap.Count);
        
        // 70% small, 25% medium, 5% large for natural distribution
        int magnitudeRoll = random.Next(100);
        Magnitude = magnitudeRoll < 70 ? StarMagnitude.Small : magnitudeRoll < 95 ? StarMagnitude.Medium : StarMagnitude.Large;
        
        // Set size and duration based on magnitude
        (Size, GlowSize, Duration) = Magnitude switch
        {
            StarMagnitude.Small => (random.Next(2, 3), random.Next(8, 10), random.NextDouble() * 1.5 + 2),
            StarMagnitude.Medium => (random.Next(3, 5), random.NextDouble() * 2 + 12, random.NextDouble() * 1.2 + 2.5),
            StarMagnitude.Large => (random.Next(5, 8), random.NextDouble() * 3 + 18, random.NextDouble() * 1 + 3),
            _ => (3, 12, 2.5)
        };
        
        Delay = random.NextDouble() * 4; // 0s to 4s delay

        // Set start and end positions based on edge origin
        switch (Origin)
        {
            case EdgeOrigin.Left:
                StartLeft = random.Next(-30, -10);
                StartTop = random.Next(10, 90);
                EndLeft = random.Next(110, 130);
                EndTop = StartTop + random.Next(-20, 21);
                break;

            case EdgeOrigin.Right:
                StartLeft = random.Next(110, 130);
                StartTop = random.Next(10, 90);
                EndLeft = random.Next(-30, -10);
                EndTop = StartTop + random.Next(-20, 21);
                break;

            case EdgeOrigin.Top:
                StartLeft = random.Next(-10, 110);
                StartTop = random.Next(-30, -10);
                EndLeft = StartLeft + random.Next(-20, 21);
                EndTop = random.Next(110, 130);
                break;

            case EdgeOrigin.Bottom:
                StartLeft = random.Next(-10, 110);
                StartTop = random.Next(110, 130);
                EndLeft = StartLeft + random.Next(-20, 21);
                EndTop = random.Next(-30, -10);
                break;
        }
    }

    public string GetAnimationStyle()
    {
        if (!ColorMap.TryGetValue(Color, out var colors))
        {
            // Fallback to white if color not found
            colors = ColorMap[GlowColor.White];
        }

        var (rgb, glowRgb) = colors;
        return $"--star-start-left: {StartLeft}%; " +
               $"--star-start-top: {StartTop}%; " +
               $"--star-end-left: {EndLeft}%; " +
               $"--star-end-top: {EndTop}%; " +
               $"--star-duration: {Duration}s; " +
               $"--star-delay: {Delay}s; " +
               $"--star-size: {Size}px; " +
               $"--star-glow: {GlowSize}px; " +
               $"--star-color: rgb({rgb}); " +
               $"--star-glow-color: rgba({glowRgb}, 0.8);";
    }
}