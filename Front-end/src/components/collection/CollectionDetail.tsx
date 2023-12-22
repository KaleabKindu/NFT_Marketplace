import Image from "next/image"
import { TypographyH2, TypographyH3, TypographyP, TypographySmall } from "../common/Typography"

type Props = {}

const CollectionDetail = (props: Props) => {
  return (
    <div className='relative flex flex-col lg:flex-row gap-8 -mt-[15vh] w-[90%] lg:w-[85%] mx-auto bg-background border z-40 rounded-3xl p-8'>
        <div className="relative w-full h-[300px] lg:w-[350px] lg:h-[300px]">
            <Image className="rounded-3xl object-cover" src='/collection/collection-pic.png' fill alt=''/>
        </div>
        <div className="flex flex-col gap-5">
            <TypographyH2 text='Awesome NFT Collection'/>
            <TypographyP text='Karafuru is home to 5,555 generative arts where colors reign supreme. Leave the drab reality and enter the world of Karafuru by Museum of Toys.'/>
            <div className="grid grid-cols-2 lg:grid-cols-4 gap-5">
                <div className="flex flex-col items-center gap-5 lg:gap-8 p-5 lg:p-8 border rounded-xl">
                    <TypographySmall className="text-accent-foreground/60" text='Floor Price'/>
                    <TypographyH3 className="text-accent-foreground/60" text={`${0.5475} ETH`}/>
                </div>
                <div className="flex flex-col items-center gap-5 lg:gap-8 p-5 lg:p-8 border rounded-xl">
                    <TypographySmall className="text-accent-foreground/60" text='Volume'/>
                    <TypographyH3 className="text-accent-foreground/60" text={`${0.5475} ETH`}/>
                </div>
                <div className="flex flex-col items-center gap-5 lg:gap-8 p-5 lg:p-8 border rounded-xl">
                    <TypographySmall className="text-accent-foreground/60" text='Latest Price'/>
                    <TypographyH3 className="text-accent-foreground/60" text={`${0.5475} ETH`}/>
                </div>
                <div className="flex flex-col items-center gap-5 lg:gap-8 p-5 lg:p-8 border rounded-xl">
                    <TypographySmall className="text-accent-foreground/60" text='Items'/>
                    <TypographyH3 className="text-accent-foreground/60" text={2270}/>
                </div>
            </div>
        </div>
    </div>
  )
}

export default CollectionDetail