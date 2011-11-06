using System;
using System.ComponentModel.Composition;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using TEdit.Common;
using TEdit.RenderWorld;
using TEdit.TerrariaWorld;
using System.Collections.Generic;

namespace TEdit.Tools.Tool
{
    [Export(typeof(ITool))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    [ExportMetadata("Order", 7)]
    public class DungeonPointPicker : ToolBase
    {
        [Import]
        private ToolProperties _properties;

        [Import("World", typeof(World))]
        private World _world;

        public DungeonPointPicker()
        {
            _Image = new BitmapImage(new Uri(@"pack://application:,,,/TEdit;component/Images/Tools/dungeon_tool.png"));
            _Name = "Dungeon Point Tool";
            _Type = ToolType.Pencil;
            IsActive = false;
        }

        #region Properties

        private readonly BitmapImage _Image;
        private readonly string _Name;

        private readonly ToolType _Type;
        private bool _IsActive;

        public override string Name
        {
            get { return _Name; }
        }

        public override ToolType Type
        {
            get { return _Type; }
        }

        public override BitmapImage Image
        {
            get { return _Image; }
        }

        public override bool IsActive
        {
            get { return _IsActive; }
            set
            {
                if (_IsActive != value)
                {
                    _IsActive = value;
                    RaisePropertyChanged("IsActive");
                }
                if (_IsActive)
                {
                    _properties.MinHeight = 1;
                    _properties.MinWidth = 1;
                    _properties.MaxHeight = 1;
                    _properties.MaxWidth = 1;
                }
            }
        }

        #endregion

        public override bool PressTool(TileMouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                SetSpawn(e);
            }

            return true;
        }


        public override bool MoveTool(TileMouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                SetSpawn(e);
            }
            return true;
        }

        private void SetSpawn(TileMouseEventArgs e)
        {
            if (!WorldSettings.Tiles[_world.Tiles[e.Tile.X, e.Tile.Y].Type].IsSolid ||
                !_world.Tiles[e.Tile.X, e.Tile.Y].IsActive)
            {
                _world.Header.DungeonEntrance = e.Tile;
            }
        }

        public override bool ReleaseTool(TileMouseEventArgs e)
        {
            // Do nothing on release
            return false;
        }

        public override WriteableBitmap PreviewTool() {
            return new WriteableBitmap(
                1,
                1,
                96,
                96,
                PixelFormats.Bgra32,
                null);
        }

    }
}