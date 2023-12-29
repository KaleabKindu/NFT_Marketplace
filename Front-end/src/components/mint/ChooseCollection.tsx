'use client'
import { useState } from "react";
import { Avatar } from "../common/Avatar"
import { TypographyH4, TypographyP, TypographySmall } from "../common/Typography"
import { ScrollArea, ScrollBar } from "../ui/scroll-area"
import { IoCheckmarkCircle } from "react-icons/io5";
import { Button } from "../ui/button";
import { MdAdd } from "react-icons/md";
import { cn } from "@/lib/utils";
type Props = {}

const ChooseCollection = (props: Props) => {
    const [ selected, setSelected ] = useState(0)
  return (
    <div>
        <TypographyH4 text='Choose Collection'/>
        <TypographySmall text='Choose an exiting collection or create a new one'/>
        <ScrollArea>
            <div className="flex gap-5 mt-5">
                {
                    Array.from({length:3}).map((_, index) => 
                     <Button key={index} variant={index + 1 === selected ?'default':'ghost'} className="flex flex-col gap-5 p-5 border whitespace-normal text-left items-stretch rounded-md  h-auto w-[15rem]" onClick={() => setSelected(index + 1)}>
                        <div className="flex justify-between items-center">
                            <Avatar className="w-10 h-10"/>
                            {index + 1 === selected && <IoCheckmarkCircle size={30}/>}
                        </div>
                        <TypographyP className={cn({"text-foreground":index + 1 === selected})} text='Crypto Legend Professor'/>
                     </Button>
                    )
                }
                <Button variant={'ghost'} className="flex flex-col gap-5 p-5 border whitespace-normal text-left items-center rounded-md  h-auto w-[15rem]">
                    <MdAdd size={30} />
                </Button>
            </div>
            <ScrollBar className="hidden" orientation="horizontal"/>
        </ScrollArea>
    </div>
  )
}

export default ChooseCollection