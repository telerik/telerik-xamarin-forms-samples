using System.Collections.Generic;
using System.Linq;
using tagit.Analysis;
using tagit.Models;

namespace tagit
{
    internal static class ItemExtensions
    {
        internal static void ApplyAnalysis(this ImageInformation image, ImageAnalysisResult analysis)
        {
            image.Caption = analysis.caption;
            image.Tags = analysis.tags;
            image.Categories = analysis.details.categories?.Select(s => s.name.Replace("_", " ")).ToList() ??
                               new List<string>();

            image.IsAdult = analysis.details.adult.isAdultContent;
            image.AdultScore = analysis.details.adult.adultScore;
            image.IsRacy = analysis.details.adult.isRacyContent;
            image.RacyScore = analysis.details.adult.racyScore;

            image.Faces = (from face in analysis.details.faces
                select new FaceInformation
                {
                    Age = face.age,
                    Gender = face.gender
                }).ToList();

            image.AccentColor = $"#{analysis.details.color.accentColor}";
            image.BackgroundColor = $"{analysis.details.color.dominantColorBackground}";
            image.ForegroundColor = $"{analysis.details.color.dominantColorForeground}";
            image.IsBlackAndWhite = analysis.details.color.isBWImg;
            image.IsClipArt = analysis.details.imageType.clipArtType > 0;
            image.IsLineDrawing = analysis.details.imageType.lineDrawingType > 0;

            image.IsProcessing = false;
            image.IsTagged = true;
        }
    }
}