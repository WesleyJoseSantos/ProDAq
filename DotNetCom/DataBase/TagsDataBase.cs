using DotNetCom.General.Tags;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DotNetCom.DataBase
{
    public class TagsDataBase
    {
        [JsonProperty]
        public Dictionary<string, Tag> Tags { get; set; } = new Dictionary<string, Tag>();
        [JsonProperty]
        public Dictionary<string, TagGroup> TagsGroups { get; set; } = new Dictionary<string, TagGroup>();

        public void Add(Tag tag)
        {
            if (tag == null) return;
            if (tag.Name == null) return;
            if (tag.Name == "") return;
            try
            {
                if (!Tags.ContainsKey(tag.Name))
                {
                    Tags.Add(tag.Name, tag);
                }
                else
                {
                    //MessageBox.Show("Tag Name already exists.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
        }

        public void Remove(Tag tag)
        {
            if (tag == null) return;
            if (tag.Name == null) return;
            if (tag.Name == "") return;
            try
            {
                if (!Tags.ContainsKey(tag.Name))
                {
                    Tags.Remove(tag.Name);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
        }

        public void Add(TagGroup tagsGroup)
        {
            if (tagsGroup == null) return;
            if (tagsGroup.Name == null) return;
            if (tagsGroup.Name == "") return;
            try
            {
                if (!TagsGroups.ContainsKey(tagsGroup.Name))
                {
                    TagsGroups.Add(tagsGroup.Name, tagsGroup);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
        }

        public void Remove(TagGroup tagsGroup)
        {
            if (tagsGroup == null) return;
            if (tagsGroup.Name == null) return;
            if (tagsGroup.Name == "") return;
            try
            {
                if (!TagsGroups.ContainsKey(tagsGroup.Name))
                {
                    TagsGroups.Remove(tagsGroup.Name);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
        }

        public Tag[] GetTags(string[] tags)
        {
            var list = new List<Tag>();
            foreach (var tag in tags)
            {
                if (Tags.ContainsKey(tag ?? ""))
                {
                    list.Add(Tags[tag]);
                }
            }
            return list.ToArray();
        }

        //public Tag[] GetTags(Tag[] tags)
        //{
        //    var list = new List<Tag>();
        //    foreach (var tag in tags)
        //    {
        //        if (Tags.ContainsKey(tag.Name ?? ""))
        //        {
        //            list.Add(Tags[tag.Name]);
        //        }
        //    }
        //    return list.ToArray();
        //}

        //public Tag GetTag(Tag tag)
        //{
        //    if (Tags.ContainsKey(tag.Name))
        //    {
        //        return Tags[tag.Name];
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}

        public Tag GetTag(string tagName)
        {
            if (Tags.ContainsKey(tagName))
            {
                return Tags[tagName];
            }
            else
            {
                return null;
            }
        }

    }
}
