using System;

namespace WpfApp1
{
    public class JPGInformation
    {
        public string Text { get; set; }

        public int FindSOF(byte[] data){
            for(int i = 0; i<(data.Length-1); i++ ){
                if(data[i] == 0xff && (data[i+1] == 0xC0 || data[i+1] == 0xC2)){
                return i;
                }
            }
            return -1;
        }

        public int ReturnJPGBitDepth(byte[] data, int startFrame){
            return data[startFrame + 4];
        }

        public int ReturnJPGHeight(byte[] data, int startFrame){
            return data[startFrame + 5]*16*16 + data[startFrame + 6];;
        }

        public int ReturnJPGWidth(byte[] data, int startFrame){
            return data[startFrame + 7]*16*16 + data[startFrame + 8];;
        }

        public string ReturnJPGType(byte[] data, int startFrame){
            switch(data[startFrame + 9]){
                case 0x01:
                return "Monochromatic";
                break;
                case 0x03:
                return "Color YcbCr or YIQ";
                break;
                case 0x04:
                return "CMYK";
                break;
                deafault:
                break;
            }
            return " ";
         }

         public int ReturnJPGComponentNumber(byte[] data, int startFrame){
            return data[startFrame + 9];
         }

        public byte[] ReturnJPGComponentBytes(byte[] data, int startFrame){
            
            byte[] component = new byte[((int)data[(startFrame + 9)])*3 + 1];
            for(int i = 0; i<component.Length; i++){
                component[i] = data[startFrame + 9 + i];
            }
            return component;
        }

        public int FindSOS(byte[] data){
            for(int i = 0; i<(data.Length-1); i++ ){
                if(data[i] == 0xff && (data[i+1] == 0xDA)){
                return i;
                }
            }
            return -1;
        }

        public byte[] ReturnJPGData(byte[] image, int startFrame, int numberOfComponent){

            int a = 0;
            for (int i = startFrame + 1; i < (image.Length - 1); i++)
            {
                if (image[i] == 0xff && (image[i+1] != 0x00 || image[i + 1] != 0xD0 || image[i + 1] != 0xD1 || image[i + 1] != 0xD2 || image[i + 1] != 0xD3 || image[i + 1] != 0xD4 || image[i + 1] != 0xD5 || image[i + 1] != 0xD6 || image[i + 1] != 0xD7) )
                {
                    a = i;
                    break;
                }
            }

            byte[] data = new byte[a - 2 - startFrame - 3 - 2*numberOfComponent - 2 - 2 ];
            
            for (int i = 0; i<data.Length; i++){
                data[i] = image[startFrame + 8 + 2*numberOfComponent + i];
            }
            return data;
        }

        public void ChangeJPGData(byte[] image, int startFrame, int numberOfComponent, byte[] data){
            
            for(int i = 0; i<data.Length; i++){
                image[startFrame + 8 + 2*numberOfComponent + i] = data[i];
            }
        }
    }
}
