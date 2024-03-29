﻿using System;
using System.Collections.Generic;
using System.Linq;

using UnityEditor;
using UnityEngine;

using DecentM.YTdlp;
using DecentM.Shared.Editor;
using DecentM.Shared.Icons;
using DecentM.VideoPlayer.Plugins;

namespace DecentM.VideoPlayer.Editor
{
    [CustomEditor(typeof(VideoPlaylist))]
    public class VideoPlaylistInspector : Inspector
    {
        public const int UrlHeight = 150;
        public const int Padding = 8;
        public const int ButtonsWidth = 50;

        private string importPlaylistUrl = "";

        public override void OnInspectorGUI()
        {
            this.DrawDefaultInspector();

            VideoPlaylist playlist = (VideoPlaylist)target;

            Rect toolbarRectOuter = this.DrawRegion(50, new Vector4(0, Padding, 0, Padding));
            Rect toolbarRectInner = this.GetRectInside(
                toolbarRectOuter,
                new Vector2(toolbarRectOuter.width, toolbarRectOuter.height),
                new Vector4(Padding, Padding, Padding, 0)
            );

            int toolbarButtons = 3;
            int toolbarButtonCount = 0;

            Rect refreshAllButton = this.GetRectInside(
                toolbarRectInner,
                new Vector2(toolbarRectInner.width / toolbarButtons, toolbarRectInner.height),
                new Vector4(toolbarRectInner.width / toolbarButtons * toolbarButtonCount, 0)
            );

            toolbarButtonCount++;

            Rect clearButton = this.GetRectInside(
                toolbarRectInner,
                new Vector2(toolbarRectInner.width / toolbarButtons, toolbarRectInner.height),
                new Vector4(toolbarRectInner.width / toolbarButtons * toolbarButtonCount, 0)
            );
            EditorGUI.BeginDisabledGroup(playlist.urls.Length == 0);
            if (this.ToolbarButton(clearButton, "Clear playlist"))
            {
                this.Clear();
            }
            EditorGUI.EndDisabledGroup();

            toolbarButtonCount++;

            Rect reimportMetadataButton = this.GetRectInside(
                toolbarRectInner,
                new Vector2(toolbarRectInner.width / toolbarButtons, toolbarRectInner.height),
                new Vector4(toolbarRectInner.width / toolbarButtons * toolbarButtonCount, 0)
            );
            if (this.ToolbarButton(reimportMetadataButton, "Reimport metadata"))
                this.ReimportMetadata();

            Rect importRegion = this.DrawRegion(
                42,
                new Vector4(Padding, Padding, Padding, Padding)
            );
            Rect importTextField = this.GetRectInside(
                importRegion,
                new Vector2(importRegion.width / 6 * 5, importRegion.height)
            );
            Rect importButton = this.GetRectInside(
                importRegion,
                new Vector2(importRegion.width / 6, importRegion.height),
                new Vector4(importTextField.width, 0)
            );
            this.importPlaylistUrl = EditorGUI.TextField(importTextField, this.importPlaylistUrl);
            EditorGUI.BeginDisabledGroup(this.importPlaylistUrl == "");
            if (this.Button(importButton, "Import playlist"))
            {
                this.ImportPlaylist();
            }
            EditorGUI.EndDisabledGroup();

            Rect statsRegion = this.DrawRegion(
                36,
                new Vector4(Padding, Padding, Padding * 2, Padding)
            );
            this.DrawLabel(statsRegion, $"{playlist.urls.Length} videos loaded", 3);

            if (playlist.urls.Length == 0)
            {
                Rect insertButton = this.DrawRegion(
                    25,
                    new Vector4(Padding, Padding, Padding * 2, 0)
                );
                if (this.Button(insertButton, MaterialIcons.GetIcon(Icon.Plus)))
                    this.AddNew();
            }

            for (int i = 0; i < playlist.urls.Length; i++)
            {
                if (i < 0 || i >= playlist.urls.Length)
                    continue;

                object[] item = playlist.urls[i];
                if (item == null)
                    continue;

                string url = (string)item[0];
                Sprite thumbnail = (Sprite)item[1];
                string title = (string)item[2];
                string uploader = (string)item[3];
                string platform = (string)item[4];
                int views = (int)item[5];
                int likes = (int)item[6];
                string resolution = (string)item[7];
                int fps = (int)item[8];
                string description = (string)item[9];
                string duration = (string)item[10];
                TextAsset[] subtitles = (TextAsset[])item[11];

                Rect regionOuter = this.DrawRegion(UrlHeight, new Vector4(0, 0, 0, 0));
                Rect region = this.GetRectInside(
                    regionOuter,
                    new Vector4(Padding, 0, Padding, Padding * 1.5f)
                );

                EditorGUI.DrawRect(region, new Color(38 / 255f, 38 / 255f, 38 / 255f));

                Rect orderingButtonsRectOuter = this.GetRectInside(
                    region,
                    new Vector2(ButtonsWidth, region.height)
                );
                Rect orderingButtonsRectInner = this.GetRectInside(
                    orderingButtonsRectOuter,
                    new Vector4(0, 0, 0, 0)
                );

                int buttonCount = 0;
                int buttons = 4;
                float buttonHeight = orderingButtonsRectInner.height / buttons;
                float buttonPadding = 0;

                Rect topButton = this.GetRectInside(
                    orderingButtonsRectInner,
                    new Vector2(orderingButtonsRectInner.width, buttonHeight),
                    new Vector4(
                        buttonPadding,
                        buttonCount * buttonHeight,
                        buttonPadding,
                        buttonPadding
                    )
                );
                EditorGUI.BeginDisabledGroup(i <= 0);
                if (this.Button(topButton, MaterialIcons.GetIcon(Icon.ChevronDoubleUp)))
                {
                    this.SwapIndexes(i, 0);
                    i = 0;
                    continue;
                }
                EditorGUI.EndDisabledGroup();

                buttonCount++;

                Rect upButton = this.GetRectInside(
                    orderingButtonsRectInner,
                    new Vector2(orderingButtonsRectInner.width, buttonHeight),
                    new Vector4(
                        buttonPadding,
                        buttonCount * buttonHeight,
                        buttonPadding,
                        buttonPadding
                    )
                );
                EditorGUI.BeginDisabledGroup(i <= 0);
                if (this.Button(upButton, MaterialIcons.GetIcon(Icon.ChevronUp)))
                {
                    this.SwapIndexes(i, i - 1);
                    i--;
                    continue;
                }
                EditorGUI.EndDisabledGroup();

                buttonCount++;

                Rect downButton = this.GetRectInside(
                    orderingButtonsRectInner,
                    new Vector2(orderingButtonsRectInner.width, buttonHeight),
                    new Vector4(
                        buttonPadding,
                        buttonCount * buttonHeight,
                        buttonPadding,
                        buttonPadding
                    )
                );
                EditorGUI.BeginDisabledGroup(i >= playlist.urls.Length - 1);
                if (this.Button(downButton, MaterialIcons.GetIcon(Icon.ChevronDown)))
                {
                    this.SwapIndexes(i, i + 1);
                    i--;
                    continue;
                }
                EditorGUI.EndDisabledGroup();

                buttonCount++;

                Rect bottomButton = this.GetRectInside(
                    orderingButtonsRectInner,
                    new Vector2(orderingButtonsRectInner.width, buttonHeight),
                    new Vector4(
                        buttonPadding,
                        buttonCount * buttonHeight,
                        buttonPadding,
                        buttonPadding
                    )
                );
                EditorGUI.BeginDisabledGroup(i >= playlist.urls.Length - 1);
                if (this.Button(bottomButton, MaterialIcons.GetIcon(Icon.ChevronDoubleDown)))
                {
                    this.SwapIndexes(i, playlist.urls.Length - 1);
                    i--;
                    continue;
                }
                EditorGUI.EndDisabledGroup();

                Rect thumbnailRectOuter = this.GetRectInside(
                    region,
                    new Vector2(region.width / 3, region.height),
                    new Vector4(orderingButtonsRectInner.width + Padding, 0, 0, 0)
                );
                Rect thumbnailRectInner = this.GetRectInside(
                    thumbnailRectOuter,
                    new Vector4(Padding, Padding, Padding, Padding)
                );

                if (thumbnail != null && thumbnail.texture != null)
                {
                    float thumbnailAspectRatio =
                        1f * thumbnail.texture.height / thumbnail.texture.width;
                    thumbnailRectInner.width = thumbnailRectInner.height / thumbnailAspectRatio;
                    this.DrawImage(thumbnail.texture, thumbnailRectInner);
                }
                else
                {
                    this.DrawImage(new Texture2D(192, 108), thumbnailRectInner);
                }

                Rect textRectOuter = this.GetRectInside(
                    region,
                    new Vector2(
                        region.width - thumbnailRectInner.width - (ButtonsWidth * 2),
                        region.height
                    ),
                    new Vector4(
                        thumbnailRectInner.width + orderingButtonsRectOuter.width + Padding * 2,
                        0,
                        0,
                        0
                    )
                );
                Rect textRectInner = this.GetRectInside(
                    textRectOuter,
                    new Vector4(0, Padding, Padding, Padding)
                );

                int count = 0;
                int texts = 6;
                float height = textRectInner.height / texts;

                Rect titleRect = this.GetRectInside(
                    textRectInner,
                    new Vector2(textRectInner.width, height + 4),
                    new Vector4(Padding, count * height - 4, Padding, 0)
                );
                this.DrawLabel(titleRect, title, 3, FontStyle.Bold);

                count++;

                Rect uploaderRect = this.GetRectInside(
                    textRectInner,
                    new Vector2(textRectInner.width, height),
                    new Vector4(Padding, count * height, Padding, 0)
                );
                List<string> uploaderLabels = new List<string>();
                if (uploader != null && uploader != "")
                    uploaderLabels.Add(uploader);
                if (platform != null && uploader != "")
                    uploaderLabels.Add(platform);
                this.DrawLabel(uploaderRect, string.Join(" - ", uploaderLabels.ToArray()), 2);

                count++;

                Rect countersRect = this.GetRectInside(
                    textRectInner,
                    new Vector2(textRectInner.width, height),
                    new Vector4(Padding, count * height, Padding, 0)
                );
                List<string> counterLabels = new List<string>();
                if (views != 0)
                    counterLabels.Add($"{views} views");
                if (likes != 0)
                    counterLabels.Add($"{likes} likes");
                this.DrawLabel(countersRect, string.Join(", ", counterLabels.ToArray()), 2);

                count++;

                Rect specsRect = this.GetRectInside(
                    textRectInner,
                    new Vector2(textRectInner.width, height),
                    new Vector4(Padding, count * height, Padding, 0)
                );
                List<string> techLabels = new List<string>();
                if (resolution != null)
                    techLabels.Add(resolution);
                if (fps != 0)
                    techLabels.Add($"{fps}fps");
                this.DrawLabel(specsRect, string.Join("@", techLabels.ToArray()), 2);

                count++;

                Rect subsRect = this.GetRectInside(
                    textRectInner,
                    new Vector2(textRectInner.width, height),
                    new Vector4(Padding, count * height, Padding, 0)
                );
                this.DrawLabel(
                    subsRect,
                    subtitles == null
                      ? ""
                      : string.Join(", ", subtitles.Select((asset) => asset.name).ToArray()),
                    2
                );

                count++;

                Rect urlRect = this.GetRectInside(
                    textRectInner,
                    new Vector2(textRectInner.width, height),
                    new Vector4(Padding, count * height, Padding, 0)
                );
                item[0] = EditorGUI.TextField(urlRect, url.ToString());

                Rect actionButtonsRectOuter = this.GetRectInside(
                    region,
                    new Vector2(ButtonsWidth, region.height),
                    new Vector4(
                        orderingButtonsRectOuter.width
                            + thumbnailRectInner.width
                            + textRectOuter.width,
                        0,
                        0,
                        0
                    )
                );
                Rect actionButtonsRectInner = this.GetRectInside(
                    actionButtonsRectOuter,
                    new Vector4(0, 0, 0, 0)
                );

                buttonCount = 0;
                buttons = 2;
                buttonHeight = actionButtonsRectInner.height / buttons;

                Rect refreshButton = this.GetRectInside(
                    actionButtonsRectInner,
                    new Vector2(actionButtonsRectInner.width, buttonHeight),
                    new Vector4(
                        buttonPadding,
                        buttonCount * buttonHeight,
                        buttonPadding,
                        buttonPadding
                    )
                );

                Rect removeButton = this.GetRectInside(
                    actionButtonsRectInner,
                    new Vector2(actionButtonsRectInner.width, buttonHeight),
                    new Vector4(
                        buttonPadding,
                        buttonCount * buttonHeight,
                        buttonPadding,
                        buttonPadding
                    )
                );
                if (this.Button(removeButton, MaterialIcons.GetIcon(Icon.Close)))
                {
                    this.RemoveUrl(i);
                    i--;
                    continue;
                }

                Rect insertButton = this.GetRectInside(
                    region,
                    new Vector2(region.width, 25),
                    new Vector4(0, region.height, 0, 0)
                );
                if (this.Button(insertButton, MaterialIcons.GetIcon(Icon.Plus)))
                    this.InsertAfterIndex(i);

                playlist.urls[i] = item;
            }

            EditorGUILayout.GetControlRect();
        }

        private void ReimportMetadata()
        {
            this.BakeMetadata();
        }

        private void ImportPlaylistCallback(YTDLFlatPlaylistJson jsonOrNull)
        {
            VideoPlaylist playlist = (VideoPlaylist)target;

            if (jsonOrNull as YTDLFlatPlaylistJson? == null)
            {
                EditorUtility.DisplayDialog(
                    "Import error",
                    "Could not retrieve videos from this playlist. Make sure the URL points to a playlist and not just a video",
                    "OK"
                );
                return;
            }

            YTDLFlatPlaylistJson json = (YTDLFlatPlaylistJson)jsonOrNull;

            if (playlist.urls.Length != 0)
            {
                bool shouldOverwrite = EditorUtility.DisplayDialog(
                    "Playlist import",
                    "There are already videos on the playlist. Do you want to clear them, or append the playlist?",
                    "Overwrite",
                    "Append"
                );
                if (shouldOverwrite)
                    this.Clear();
            }

            for (int i = 0; i < json.entries.Length; i++)
            {
                YTDLFlatPlaylistJsonEntry entry = json.entries[i];
                AsyncProgress.Display(
                    $"Adding URLs... ({i} of {json.entries.Length})",
                    1f * i / json.entries.Length
                );
                this.AddNew(entry.url);
            }

            playlist.title = json.title;
            playlist.author = json.uploader;
            playlist.description = json.description;

            AsyncProgress.Clear();
            this.importPlaylistUrl = "";
            this.RemoveEmptyUrls();
        }

        private void ImportPlaylist()
        {
            GUI.FocusControl(null);
            YTdlpCommands.GetPlaylistVideos(this.importPlaylistUrl, this.ImportPlaylistCallback);
        }

        private void Clear()
        {
            GUI.FocusControl(null);
            VideoPlaylist playlist = (VideoPlaylist)target;

            playlist.urls = new object[0][];
        }

        private void RemoveUrl(int index)
        {
            GUI.FocusControl(null);

            VideoPlaylist playlist = (VideoPlaylist)target;

            List<object[]> list = playlist.urls.ToList();
            list.RemoveAt(index);
            playlist.urls = list.ToArray();
        }

        private void SwapIndexes(int indexA, int indexB)
        {
            GUI.FocusControl(null);

            VideoPlaylist playlist = (VideoPlaylist)target;
            object[][] newUrls = new object[playlist.urls.Length][];

            Array.Copy(playlist.urls, newUrls, playlist.urls.Length);
            newUrls[indexB] = playlist.urls[indexA];
            newUrls[indexA] = playlist.urls[indexB];

            playlist.urls = newUrls;
        }

        private object[] CreateNewItem(
            string url,
            Sprite thumbnail,
            string title,
            string uploader,
            string platform,
            int views,
            int likes,
            string resolution,
            int fps,
            string description,
            string duration,
            TextAsset[] subtitles
        )
        {
            return new object[]
            {
                url,
                thumbnail,
                title,
                uploader,
                platform,
                views,
                likes,
                resolution,
                fps,
                description,
                duration,
                subtitles
            };
        }

        private object[] CreateNewItem()
        {
            Texture2D texture = new Texture2D(192, 108);

            return this.CreateNewItem(
                "",
                Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f),
                "",
                "",
                "",
                0,
                0,
                "",
                0,
                "",
                "",
                new TextAsset[] { }
            );
        }

        private object[] CreateNewItem(string url)
        {
            Texture2D texture = new Texture2D(192, 108);

            return this.CreateNewItem(
                url,
                Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f),
                "",
                "",
                "",
                0,
                0,
                "",
                0,
                "",
                "",
                new TextAsset[] { }
            );
        }

        private void InsertAfterIndex(int index)
        {
            GUI.FocusControl(null);

            VideoPlaylist playlist = (VideoPlaylist)target;
            List<object[]> list = playlist.urls.ToList();

            list.Insert(index + 1, this.CreateNewItem());

            playlist.urls = list.ToArray();
        }

        private void AddNew()
        {
            this.AddNew("");
        }

        private void AddNew(string url)
        {
            GUI.FocusControl(null);

            VideoPlaylist playlist = (VideoPlaylist)target;
            List<object[]> list = playlist.urls.ToList();

            list.Add(this.CreateNewItem(url));

            playlist.urls = list.ToArray();
        }

        private void RemoveEmptyUrls()
        {
            VideoPlaylist playlist = (VideoPlaylist)target;

            bool showProgress = playlist.urls.Length > 35;

            for (int i = 0; i < playlist.urls.Length; i++)
            {
                if (showProgress)
                    AsyncProgress.Display(
                        $"Checking integrity {i} / {playlist.urls.Length}",
                        1f * i / playlist.urls.Length
                    );
                object[] item = playlist.urls[i];

                if (item == null || item[0] == null)
                    this.RemoveUrl(i);
            }

            AsyncProgress.Clear();
        }

        private void BakeMetadata()
        {
            VideoPlaylist playlist = (VideoPlaylist)target;

            bool showProgress = playlist.urls.Length > 35;

            for (int i = 0; i < playlist.urls.Length; i++)
            {
                if (showProgress)
                    AsyncProgress.Display(
                        $"Baking playlist {i} / {playlist.urls.Length}",
                        1f * i / playlist.urls.Length
                    );

                object[] item = playlist.urls[i];
                if (item == null)
                    continue;
                string url = (string)item[0];
                if (url == null)
                    continue;

                object[] newItem = this.CreateNewItem(
                    url
                );

                playlist.urls[i] = newItem;
            }

            this.SaveModifications();

            AsyncProgress.Clear();
        }
    }
}
