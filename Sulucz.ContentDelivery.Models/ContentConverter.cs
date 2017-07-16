namespace Sulucz.ContentDelivery.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using HeyRed.MarkdownSharp;

    using Sulucz.ContentDelivery.Data;
    using Sulucz.ContentDelivery.Data.Models;

    /// <summary>
    /// The content converter.
    /// </summary>
    internal static class ContentConverter
    {
        /// <summary>
        /// The markdown processor.
        /// </summary>
        private static readonly Markdown Markdown = new Markdown(new MarkdownOptions())
                                               {
                                                   // No options for now
                                               };

        /// <summary>
        /// Converts post content to a single block.
        /// </summary>
        /// <param name="contents">The contents.</param>
        /// <returns>The result <see cref="string"/>.</returns>
        public static string ConvertContent(IEnumerable<SuluczPostContent> contents)
        {
            var result = new StringBuilder();
            foreach (var content in contents)
            {
                switch (content.ContentType)
                {
                    case SuluczContentType.Markdown:
                        result.Append(ContentConverter.Markdown.Transform(content.Content));
                        break;

                    default:
                        result.Append(content.Content);
                        break;
                }
            }

            return result.ToString();
        }
    }
}
