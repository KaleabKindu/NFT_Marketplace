'use client'
import Image from "next/image"
import { TypographyH4, TypographyP, TypographySmall } from "../common/Typography"
import { useState } from "react"
import { Badge } from "../ui/badge"
import { Button } from "../ui/button"
import { FaHeart } from "react-icons/fa"
import {
    Accordion,
    AccordionContent,
    AccordionItem,
    AccordionTrigger,
  } from "@/components/ui/accordion"
import Link from "next/link"
  
type Props = {}

const NFTDetailLeft = (props: Props) => {
    const [liked, setLiked ] = useState(false)
    const [ likes, setLikes ] = useState(22)

    const handleLikes = () => {
        setLiked(!liked)
        if(liked){
            setLikes(likes - 1)
        }else{
            setLikes(likes + 1)
        }
    }
  return (
    <div className='flex-1 flex flex-col gap-10'>
        <div className="relative h-[25rem] lg:h-[50rem]">
            <Image className='object-cover rounded-lg' src='/landing-page/audio-category.jpg' fill alt=''/>
            <Badge className='flex items-center gap-3 absolute top-5 right-5 bg-background/30 hover:bg-background text-foreground' >
                <Button variant='ghost' size={'sm'} className='rounded-full h-auto p-2' onClick={handleLikes}>
                    <FaHeart className={`${liked && 'text-red-500'} p-0`} size={20} />
                </Button>
                <TypographySmall text={likes}/>
            </Badge>

        </div>
        <div>
            <Accordion type="single" collapsible defaultValue="item-1">
                <AccordionItem value="item-1">
                    <AccordionTrigger className="bg-accent text-accent-foreground px-5 rounded-md mb-5">Description</AccordionTrigger>
                    <AccordionContent className="px-5">
                    Unleash your inner meowjesty with the Tatto Kitty Gang!  These 9,999 uniquely tattooed NFTs purr with purrsonality and rebellion.
                    Every swirl and line tells a story, from delicate flower vines to bold tribal markings. Find your perfect feline match, unlock 
                    exclusive perks like DAO membership and metaverse access, and join a purrfectly messy community of cat-loving rebels. 
                    Mint your Tatto Kitty and let the purrpetual rebellion begin.
                    </AccordionContent>
                </AccordionItem>

                <AccordionItem value="item-2">
                    <AccordionTrigger className="bg-accent text-accent-foreground px-5 rounded-md mb-5 border-b">Details</AccordionTrigger>
                    <AccordionContent className="px-5">
                        <div className="flex flex-col gap-5">
                            <div className="flex justify-between items-center">
                                <TypographyH4 text={'Contract Address: '}/>
                                <Link className="w-[50%]" href={`https://etherscan.io/address/${'0x614917f589593189ac27ac8b81064cbe450c35e3'}`}>
                                    <TypographyP className="truncate text-right" text={'0x614917f589593189ac27ac8b81064cbe450c35e3'}/>
                                </Link>
                            </div>
                            <div className="flex justify-between items-center">
                                <TypographyH4 text={'Token ID: '}/>
                                <TypographyP text='367'/>
                            </div>
                            <div className="flex justify-between items-center">
                                <TypographyH4 text={'Creator Royality: '}/>
                                <TypographyP text='5%'/>
                            </div>
                        </div>
                    </AccordionContent>
                </AccordionItem>
            </Accordion>
        </div>
    </div>
  )
}

export default NFTDetailLeft