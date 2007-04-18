//  $Id$
using System;
using DataStructures;
using SceneGraph;
using Lisp;
using LispReader;
using System.Collections.Generic;

[SupertuxObject("tilemap", "images/engine/editor/tilemap.png")]
public sealed class Tilemap : TileBlock, IGameObject, IPathObject {
	[LispChild("z-pos")]
	public int ZPos = 0;

	[PropertyProperties(Tooltip = "If selected Tux will interact with tiles in this tilemap.")]
	[LispChild("solid")]
	public bool Solid = false;

	[PropertyProperties(Tooltip = ToolTipStrings.ScriptingName)]
	[LispChild("name", Optional = true, Default = "")]
	public string Name = String.Empty;

	[LispChild("speed", Optional = true, Default = 1.0f)]
	public float Speed = 1.0f;

	[LispChild("speed-y", Optional = true, Default = 1.0f)]
	public float SpeedY = 1.0f;

	private Path path;
	[LispChild("path", Optional = true, Default = null)]
	public Path Path {
		get {
			return path;
		}
		set {
			path = value;
		}
	}

	public enum DrawTargets {
		/// <summary>
		/// Normal tilemap.
		/// </summary>
		normal,
		/// <summary>
		/// Used for lightmap.
		/// </summary>
		lightmap
	}

	/// <summary>
	/// Target for tilemap.
	/// </summary>
	[LispChild("draw-target", Optional = true, Default = DrawTargets.normal)]
	public DrawTargets DrawTarget = DrawTargets.normal;

	[PropertyProperties(Tooltip = "Opacity of this Tilemap, ranges from 0.0 (transparent) to 1.0 (fully opaque)")]
	[LispChild("alpha", Optional = true, Default = 1.0f)]
	public float Alpha = 1.0f;

	public Tilemap() : base() {
	}

}
