using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Runtime.InteropServices.ComTypes;
using MagickSharp.Core;
using MagickSharp.Core.Native;
using objection.AceEngine;

namespace objection
{
    class Program
    {
        static void Main(string[] args)
        {
            MakeFile();
        }

        public static void CompilePeople()
        {
            TextEngine engine = new TextEngine();
            engine.GetPeople();
        }

        public static void MakeFile()
        {
            var person = new Phoenix();
            var emote = person.PossibleActions.FirstOrDefault(x => x.CurrentEmote == Emotes.NORMAL);

            if (emote == null) return;

            MagickImage image =
                new MagickImage("Images/defenseempty.png");

            string ss = Console.ReadLine();
            
            var dialogue = new Dialogue(ss, person, emote, image, 
                GetText().SetText(ss));

            var collection = new MagickImageCollection();

            int step = 0;

            // make all sprites
            while (!dialogue.endReached)
            {
                MagickImage frame = new MagickImage(image);

                var ye = emote.GetCurrentSprite().GetSprite();

                frame.Composite(ye, CompositeOperator.OverCompositeOp, 0, (int)(frame.Height - ye.Height));
                ye.Dispose();

                using (MagickImage bench = new MagickImage("./Images/defense_bench.gif"))
                using (MagickImage textPopup = new MagickImage("./Images/textpopup.png"))
                {
                    frame.Composite(bench, CompositeOperator.OverCompositeOp, 0, (int)frame.Height - (int)bench.Height);
                    textPopup.Evaluate(ChannelType.AlphaChannel, EvaluateOperator.MinEvaluateOperator, 180);
                    int middle = (int)(frame.Width - textPopup.Width) / 2;
                    frame.Composite(textPopup, CompositeOperator.OverCompositeOp, middle, (int)frame.Height - (int)textPopup.Height);
                }

                // draw text
                int textCenter = ((int)frame.Width - dialogue.TextWidth) / 2;
                Console.WriteLine(dialogue.TextWidth);
                string text = dialogue.GetText();

                GetText()
                    .SetText(person.Name.First().ToString().ToUpper() + person.Name.Substring(1).ToLower())
                    .SetFontSize(14)
                    .SetPosition(13, 146)
                    .Draw(frame);
                
                if (!string.IsNullOrEmpty(text))
                {
                    
                    //12 138
                    GetText()
                        .SetText(text)
                        .SetPosition(textCenter, 160)
                        .Draw(frame);
                }

                dialogue.Advance();

                //frame.Resize(new Percentage(200));
                
                collection.Add(frame);
                collection[collection.Count - 1].ImageDelay = 8;
                Console.WriteLine($"step {++step}");
            }

            collection[collection.Count - 1].ImageDelay = 2000;
            Console.WriteLine(collection[collection.Count -1].ImageDelay);
            collection.Optimize();
            collection.Write("dialogue.gif");
            image.Dispose();

        }

        private static Drawable GetText()
        {
            return new Drawable()
                .SetFontSize(16)
                .SetFont("./Images/aceattorney.ttf")
                .SetFillColour(1, 1, 1, 1);
        }
    }
}