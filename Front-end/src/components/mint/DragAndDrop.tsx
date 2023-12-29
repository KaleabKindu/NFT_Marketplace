import React from 'react'
import { TypographyH3, TypographyP, TypographySmall } from '../common/Typography'
import { LuImagePlus } from "react-icons/lu";

type Props = {}

const DragAndDrop = (props: Props) => {
  return (
    <div>
        <TypographyH3 text='Image, Video, Audio, or 3D Model'/>
        <TypographySmall text='File types supported: JPG, PNG, GIF, SVG, MP4, WEBM, MP3, WAV, OGG, GLB, GLTF. Max size: 100 MB'/>
        <div className='flex flex-col items-center justify-center border-3 border-dashed border-foreground rounded-lg h-[20rem] mt-5 bg-accent hover:bg-accent/70'>
            <LuImagePlus size={50}/>
            <TypographyP text='Upload a file or drag and drop'/>
            <TypographySmall text='PNG, JPG, GIF up to 10MB'/>
        </div>
        
    </div>
  )
}

export default DragAndDrop