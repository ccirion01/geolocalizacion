namespace API.Enum
{
    public static class DireccionEstado
    {
        public enum DireccionEstadoEnum
        {
            PROCESANDO = 1,
            TERMINADO = 2
        }

        public static string ToString(DireccionEstadoEnum estadoEnum)
        {
            string estado = string.Empty;

            switch (estadoEnum)
            {
                case DireccionEstadoEnum.PROCESANDO:
                    estado = "Procesando";
                break;
                case DireccionEstadoEnum.TERMINADO:
                    estado = "Terminado";
                break;
            }

            return estado;
        }
    }
}
