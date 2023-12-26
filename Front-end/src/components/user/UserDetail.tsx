import Image from "next/image"
import { TypographyH2, TypographyP } from "../common/Typography"
import { TbCopy } from "react-icons/tb";
import { Button } from "../ui/button";
import { BsThreeDots } from "react-icons/bs";
import {
    DropdownMenu,
    DropdownMenuContent,
    DropdownMenuItem,
    DropdownMenuTrigger,
  } from "@/components/ui/dropdown-menu"
import { BsTwitterX, BsFacebook, BsTelegram, BsYoutube } from "react-icons/bs";
type Props = {}

const UserDetail = (props: Props) => {
  return (
    <div className='relative flex flex-col lg:flex-row gap-8 -mt-[15vh] w-[90%] lg:w-[85%] mx-auto bg-background border z-40 rounded-3xl p-8'>
        <div className="relative w-full h-[200px] lg:w-[300px] lg:h-[250px]">
            <Image className="rounded-3xl object-cover" src='/collection/collection-pic.png' fill alt=''/>
        </div>
        <div className="flex flex-col items-start gap-5">
            <TypographyH2 text='Anthony Stark'/>
            <div className="flex items-center gap-3 rounded-full px-5 py-1 bg-accent hover:bg-accent/80">
                <TypographyP text='0xASDF3456768798HJHK5768799HGJK'/>
                <TbCopy size={20}/>
            </div>
            <TypographyP text='Punk #4786 / An OG Cryptopunk Collector, hoarder of NFTs. Contributing to @ether_cards, an NFT Monetization Platform.'/>
            <div className="flex gap-5">
                <Button className="rounded-full" variant={'ghost'} size={'icon'}>
                    <BsFacebook size={25}/>
                </Button>
                <Button className="rounded-full" variant={'ghost'} size={'icon'}>
                    <BsTwitterX size={25}/>
                </Button>
                <Button className="rounded-full" variant={'ghost'} size={'icon'}>
                    <BsYoutube size={25}/>
                </Button>
                <Button className="rounded-full" variant={'ghost'} size={'icon'}>
                    <BsTelegram size={25}/>
                </Button>
            </div>
        </div>
        <div className="self-start flex items-center gap-5">
            <Button className='rounded-full'>Follow</Button>
            <DropdownMenu>
                <DropdownMenuTrigger>
                    <Button className="rounded-full" variant={'ghost'} size={'icon'}>
                        <BsThreeDots size={25}/>
                    </Button>
                </DropdownMenuTrigger>
                <DropdownMenuContent>
                    <DropdownMenuItem>Report Abuse</DropdownMenuItem>
                </DropdownMenuContent>
            </DropdownMenu>

        </div>
    </div>
  )
}

export default UserDetail