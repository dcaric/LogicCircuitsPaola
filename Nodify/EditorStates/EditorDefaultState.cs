﻿using System.Windows.Input;
using static Nodify.SelectionHelper;

namespace Nodify
{
    /// <summary>
    /// The default state of the editor.
    /// <br />
    /// <br />  Default State
    /// <br />  	- mouse left down  	-> Selecting State
    /// <br />  	- mouse right down  -> Panning State
    /// <br /> 	
    /// <br />  Selecting State
    /// <br />  	- mouse left up 	-> Default State
    /// <br />  	- mouse right down 	-> Panning State
    /// <br /> 
    /// <br />  Panning State
    /// <br />  	- mouse right up	-> previous state (Selecting State or Default State)
    /// <br />  	- mouse left up		-> Panning State
    /// <br />	
    /// </summary>
    public class EditorDefaultState : EditorState
    {
        /// <summary>Constructs an instance of the <see cref="EditorDefaultState"/> state.</summary>
        /// <param name="editor">The owner of the state.</param>
        public EditorDefaultState(NodifyEditor editor) : base(editor)
        {
        }

        /// <inheritdoc />
        public override void HandleMouseDown(MouseButtonEventArgs e)
        {
            EditorGestures.NodifyEditorGestures gestures = EditorGestures.Mappings.Editor;
            if (gestures.Cutting.Matches(e.Source, e))
            {
                PushState(new EditorCuttingState(Editor), e);
            }
            else if (gestures.Selection.Select.Matches(e.Source, e))
            {
                SelectionType selectionType = GetSelectionType(e);
                var selecting = new EditorSelectingState(Editor, selectionType);
                PushState(selecting, e);
            }
            else if (!Editor.DisablePanning && gestures.Pan.Matches(e.Source, e))
            {
                PushState(new EditorPanningState(Editor), e);
            }
        }
    }
}
