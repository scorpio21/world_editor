Imports System.Drawing

Namespace WorldEditorNet
    ' Compatibilidad para las referencias que el diseñador VB genera como
    ' WorldEditorNet.My.Resources dentro del namespace duplicado del proyecto.
    Namespace My.Resources
        Friend Module EditorResources
            Friend ReadOnly Property imagenfondo As Bitmap
                Get
                    Return Global.WorldEditorNet.My.Resources.EditorResources.imagenfondo
                End Get
            End Property

            Friend ReadOnly Property Ojo As Bitmap
                Get
                    Return Global.WorldEditorNet.My.Resources.EditorResources.Ojo
                End Get
            End Property

            Friend ReadOnly Property Ver_Cuadricula As Bitmap
                Get
                    Return Global.WorldEditorNet.My.Resources.EditorResources.Ver_Cuadricula
                End Get
            End Property

            Friend ReadOnly Property Bloqueos_Rojos As Bitmap
                Get
                    Return Global.WorldEditorNet.My.Resources.EditorResources.Bloqueos_Rojos
                End Get
            End Property

            Friend ReadOnly Property Bloqueos_Azules As Bitmap
                Get
                    Return Global.WorldEditorNet.My.Resources.EditorResources.Bloqueos_Azules
                End Get
            End Property

            Friend ReadOnly Property Triggers As Bitmap
                Get
                    Return Global.WorldEditorNet.My.Resources.EditorResources.Triggers
                End Get
            End Property

            Friend ReadOnly Property Traslados As Bitmap
                Get
                    Return Global.WorldEditorNet.My.Resources.EditorResources.Traslados
                End Get
            End Property

            Friend ReadOnly Property superficie As Bitmap
                Get
                    Return Global.WorldEditorNet.My.Resources.EditorResources.superficie
                End Get
            End Property

            Friend ReadOnly Property mapa As Bitmap
                Get
                    Return Global.WorldEditorNet.My.Resources.EditorResources.mapa
                End Get
            End Property
        End Module
    End Namespace
End Namespace
