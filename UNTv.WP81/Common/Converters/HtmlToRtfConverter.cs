using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Xml.Dom;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Documents;

namespace UNTv.WP81.Common.Converters
{
    public class HtmlToRtfConverter
    {
        public static string GetHtml(DependencyObject obj)
        {
            return (string)obj.GetValue(HtmlProperty);
        }

        public static void SetHtml(DependencyObject obj, string value)
        {
            obj.SetValue(HtmlProperty, value);
        }

        public static readonly DependencyProperty HtmlProperty =
            DependencyProperty.RegisterAttached("Html", typeof(string), typeof(HtmlToRtfConverter), new PropertyMetadata("", OnHtmlChanged));

        private static void OnHtmlChanged(DependencyObject sender, DependencyPropertyChangedEventArgs eventArgs)
        {
            var parent = (RichTextBlock)sender;
            parent.Blocks.Clear();

            var document = new XmlDocument();
            document.LoadXml((string)eventArgs.NewValue);

            ParseElement((XmlElement)(document.GetElementsByTagName("body")[0]), new RichTextBlockTextContainer(parent));
        }

        private static void ParseElement(XmlElement element, ITextContainer parent)
        {
            foreach (var child in element.ChildNodes)
            {
                if (child is Windows.Data.Xml.Dom.XmlText)
                {
                    if (string.IsNullOrEmpty(child.InnerText) ||
                        child.InnerText == "\n")
                    {
                        continue;
                    }

                    parent.Add(new Run { Text = child.InnerText });
                }
                else if (child is XmlElement)
                {
                    var e = (XmlElement)child;
                    switch (e.TagName.ToUpper())
                    {
                        case "P":
                            var paragraph = new Paragraph();
                            parent.Add(paragraph);
                            ParseElement(e, new ParagraphTextContainer(paragraph));
                            break;
                        case "STRONG":
                            var bold = new Bold();
                            parent.Add(bold);
                            ParseElement(e, new SpanTextContainer(bold));
                            break;
                        case "U":
                            var underline = new Underline();
                            parent.Add(underline);
                            ParseElement(e, new SpanTextContainer(underline));
                            break;
                        case "A":
                            ParseElement(e, parent);
                            break;
                        case "BR":
                            parent.Add(new LineBreak());
                            break;
                    }
                }


            }
        }

        private interface ITextContainer
        {
            void Add(Inline text);
            void Add(Block paragraph);
        }

        private sealed class SpanTextContainer : ITextContainer
        {
            private readonly Span _span;

            public SpanTextContainer(Span span)
            {
                _span = span;
            }

            public void Add(Inline text)
            {
                _span.Inlines.Add(text);
            }

            public void Add(Block paragraph)
            {
                throw new NotSupportedException();
            }
        }

        private sealed class ParagraphTextContainer : ITextContainer
        {
            private readonly Paragraph _block;

            public ParagraphTextContainer(Paragraph block)
            {
                _block = block;
            }

            public void Add(Inline text)
            {
                _block.Inlines.Add(text);
            }

            public void Add(Block paragraph)
            {
                throw new NotSupportedException();
            }
        }

        private sealed class RichTextBlockTextContainer : ITextContainer
        {
            private readonly RichTextBlock _richTextBlock;

            public RichTextBlockTextContainer(RichTextBlock richTextBlock)
            {
                _richTextBlock = richTextBlock;
            }

            public void Add(Inline text)
            {
                //throw new NotSupportedException();
                var paragraph = new Paragraph();
                paragraph.Inlines.Add(text);

                _richTextBlock.Blocks.Add(paragraph);
            }

            public void Add(Block paragraph)
            {
                _richTextBlock.Blocks.Add(paragraph);
            }
        }
    }
}
