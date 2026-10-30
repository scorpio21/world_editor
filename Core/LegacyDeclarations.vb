Imports System.Drawing

Namespace WorldEditorNet.Core
    ''' <summary>
    ''' Traducción estructural inicial de modDeclaraciones.bas.
    ''' No contiene todavía comportamiento de DirectX, sonido, MIDI ni ADO.
    '''
    ''' Las posiciones se mantienen como Short porque Integer de VB6 es un
    ''' entero firmado de 16 bits. Los índices de arrays se reservan con una
    ''' celda 0 cuando el formato original empieza en 1.
    ''' </summary>
    Public Module LegacyDeclarations
        Public Const MSGMod As String = "Este mapa ha sido modificado." & vbCrLf &
            "Si no lo guardas perderás todos los cambios. ¿Deseas guardarlo?"
        Public Const MSGDang As String = "¡CUIDADO! Este comando puede arruinar el mapa." & vbCrLf &
            "¿Estás seguro de que deseas continuar?"
        Public Const ENDL As String = vbCrLf

        Public Const NORTH As Byte = 1
        Public Const EAST As Byte = 2
        Public Const SOUTH As Byte = 3
        Public Const WEST As Byte = 4
        Public Const XMaxMapSize As Byte = 100
        Public Const XMinMapSize As Byte = 1
        Public Const YMaxMapSize As Byte = 100
        Public Const YMinMapSize As Byte = 1
        Public Const MAX_INVENORY_OBJS As Short = 10000
        Public Const maxDeshacer As Short = 10
        Public Const NumSoundBuffers As Byte = 20

        Public Structure tSetupMods
            Public bDinamic As Boolean
            Public byMemory As Byte
            Public bUseVideo As Boolean
            Public bNoMusic As Boolean
            Public bNoSound As Boolean
        End Structure

        Public Structure Position
            Public X As Short
            Public Y As Short
        End Structure

        Public Structure WorldPos
            Public Map As Short
            Public X As Short
            Public Y As Short
        End Structure

        Public Structure Grh
            Public GrhIndex As Short
            Public FrameCounter As Byte
            Public SpeedCounter As Byte
            Public Started As Byte
        End Structure

        Public Structure GrhDataRecord
            Public sX As Short
            Public sY As Short
            Public FileNum As Short
            Public pixelWidth As Short
            Public pixelHeight As Short
            Public TileWidth As Single
            Public TileHeight As Single
            Public NumFrames As Short
            Public Frames As Short()
            Public Speed As Short
        End Structure

        Public Structure SupData
            Public Name As String
            Public Grh As Short
            Public Width As Byte
            Public Height As Byte
            Public Block As Boolean
            Public Capa As Byte
        End Structure

        Public Structure NpcData
            Public Name As String
            Public Body As Short
            Public Head As Short
            Public Heading As Byte
        End Structure

        Public Structure ObjData
            Public Name As String
            Public ObjType As Short
            Public GrhIndex As Short
            Public GrhSecundario As Short
            Public Info As String
            Public Ropaje As Short
            Public WeaponAnim As Short
            Public ShieldAnim As Short
            Public Texto As String
        End Structure

        Public Structure MapInfoRecord
            Public Music As String
            Public Name As String
            Public MapVersion As Short
            Public Pk As Boolean
            Public SeCaenItems As Boolean
            Public Invocado As Short
            Public MagiaSinEfecto As Byte
            Public MinLevel As Short
            Public PuedeMascotas As Byte
            Public Terreno As String
            Public Zona As String
            Public Restringir As String
            Public BackUp As Byte
            Public Changed As Byte
        End Structure

        Public Structure tIndiceCuerpo
            Public Body As Short()
            Public HeadOffsetX As Short
            Public HeadOffsetY As Short
        End Structure

        Public Structure tBodyData
            Public Walk As Grh()
            Public HeadOffset As Position
        End Structure

        Public Structure tIndiceCabeza
            Public Head As Short()
        End Structure

        Public Structure tHeadData
            Public Head As Grh()
        End Structure

        Public Structure [Char]
            Public Active As Byte
            Public Heading As Byte
            Public Pos As Position
            Public Body As tBodyData
            Public Head As tHeadData
            Public Moving As Byte
            Public MoveOffset As Position
        End Structure

        Public Structure Obj
            Public ObjIndex As Short
            Public Amount As Short
        End Structure

        Public Structure MapBlock
            Public Graphic As Grh()
            Public CharIndex As Short
            Public ObjGrh As Grh
            Public Trigger As Short
            Public NPCIndex As Short
            Public OBJInfo As Obj
            Public TileExit As WorldPos
            Public Blocked As Byte
        End Structure

        Public Structure tDeshacerInfo
            Public Libre As Boolean
            Public Desc As String
        End Structure

        ' Estado global equivalente al módulo VB6. Las dimensiones se
        ' inicializan explícitamente en InitializeLegacyArrays.
        Public NombreImagen As String
        Public REFAguasArr As Short()
        Public MapData_Adyacente As MapBlock(,)
        Public SeleccionMap As MapBlock(,)
        Public DeSeleccionMap As MapBlock(,)
        Public SupDataArray As SupData()
        Public NpcDataArray As NpcData()
        Public ObjDataArray As ObjData()
        Public BodyData As tBodyData()
        Public HeadData As tHeadData()
        Public GrhData As GrhDataRecord()
        Public MapData As MapBlock(,)
        Public MapInfo As MapInfoRecord
        Public CharList As [Char]()
        Public MapData_Deshacer As MapBlock(,,)
        Public MapData_Deshacer_Info As tDeshacerInfo()

        Public MaxSup As Short
        Public NumNPCs As Short
        Public NumNPCsHOST As Short
        Public NumOBJs As Short
        Public NumBodies As Short
        Public NumMaps As Short
        Public Numheads As Short
        Public NumGrhFiles As Short
        Public MaxGrhs As Short
        Public NumChars As Short
        Public LastChar As Short

        Public IniPath As String
        Public DirGraficos As String
        Public DirMidi As String
        Public DirIndex As String
        Public DirDats As String

        Public Sub InitializeLegacyArrays()
            ReDim REFAguasArr(0 To 0)
            ReDim MapData_Adyacente(0 To XMaxMapSize, 0 To YMaxMapSize)
            ReDim SeleccionMap(0 To XMaxMapSize, 0 To YMaxMapSize)
            ReDim DeSeleccionMap(0 To XMaxMapSize, 0 To YMaxMapSize)
            ReDim SupDataArray(0 To 0)
            ReDim NpcDataArray(0 To 0)
            ReDim ObjDataArray(0 To 0)
            ReDim BodyData(0 To 0)
            ReDim HeadData(0 To 0)
            ReDim GrhData(0 To 0)
            ReDim MapData(0 To XMaxMapSize, 0 To YMaxMapSize)
            ReDim CharList(0 To 10000)
            ReDim MapData_Deshacer(0 To maxDeshacer, 0 To XMaxMapSize, 0 To YMaxMapSize)
            ReDim MapData_Deshacer_Info(0 To maxDeshacer)
        End Sub

        ' Dependencias externas deliberadamente no implementadas:
        ' Connection, DirectX7, DirectDraw7, DirectSound y DirectMusic.
        ' Se incorporarán únicamente después de definir el backend equivalente.
        Public Conexion As Object
        Public DirectX As Object
        Public DirectDraw As Object
        Public DirectSound As Object
        Public SurfaceDB As Object
    End Module
End Namespace
