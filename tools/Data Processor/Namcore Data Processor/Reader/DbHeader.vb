Namespace Reader
    Public Class DbHeader
        Private _mSignature As String
        Private _mRecordCount As UInteger
        Private _mFieldCount As UInteger
        Private _mRecordSize As UInteger
        Private _mStringBlockSize As UInteger

        Public Property Signature() As String
            Get
                Return Me._mSignature
            End Get
            Set
                Me._mSignature = Value
            End Set
        End Property

        Public Property RecordCount() As UInteger
            Get
                Return Me._mRecordCount
            End Get
            Set
                Me._mRecordCount = Value
            End Set
        End Property

        Public Property FieldCount() As UInteger
            Get
                Return Me._mFieldCount
            End Get
            Set
                Me._mFieldCount = Value
            End Set
        End Property

        Public Property RecordSize() As UInteger
            Get
                Return Me._mRecordSize
            End Get
            Set
                Me._mRecordSize = Value
            End Set
        End Property

        Public Property StringBlockSize() As UInteger
            Get
                Return Me._mStringBlockSize
            End Get
            Set
                Me._mStringBlockSize = Value
            End Set
        End Property


    End Class
End Namespace