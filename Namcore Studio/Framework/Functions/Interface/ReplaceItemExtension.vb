Imports Namcore_Studio.SpellItem_Information
Module ReplaceItemExtension
    ''' <summary>
    ''' Replaces an Item
    ''' </summary>
    <System.Runtime.CompilerServices.Extension()>
    Public Function ReplaceItem(ByRef SourceItem As Item, ByVal newitemid As Integer) As Item
        Dim itm As Item
        itm = SourceItem
        itm.id = newitemid
        itm.name = getNameOfItem(newitemid.ToString())
        itm.image = GetIconByItemId(newitemid)
        itm.rarity = GetRarityByItemId(newitemid)
        Return itm
      
    End Function

End Module
