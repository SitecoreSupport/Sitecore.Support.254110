using Sitecore.Diagnostics;
using Sitecore.Links;
using System;

namespace Sitecore.Support.Links
{
  public class LinkProvider : Sitecore.Links.LinkProvider
  {
    public new class LinkParser : Sitecore.Links.LinkProvider.LinkParser
    {
      public override string ExpandDynamicLinks(string text, UrlOptions options)
      {
        Assert.ArgumentNotNull(text, "text");
        Assert.ArgumentNotNull(options, "options");
        if (string.IsNullOrEmpty(text))
        {
          return string.Empty;
        }

        foreach (LinkExpander expander in GetExpanders())
        {
          try
          {
            expander.Expand(ref text, options);
          }
          catch (Exception e)
          {
            Log.Error("Exception occourred while parsing link", e, this);
          }
        }

        return text;
      }
    }

    protected override Sitecore.Links.LinkProvider.LinkParser CreateLinkParser()
    {
      return new LinkParser();
    }
  }
}