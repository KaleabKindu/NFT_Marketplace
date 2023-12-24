import { TypographyH2, TypographyH4, TypographyP, TypographySmall } from '@/components/common/Typography'
import DragAndDrop from '@/components/mint/DragAndDrop'
import { Input } from '@/components/ui/input'
import { Label } from '@/components/ui/label'
import { Textarea } from '@/components/ui/textarea'
import { Switch } from "@/components/ui/switch"
import { Button } from '@/components/ui/button'
import ChooseCollection from '@/components/mint/ChooseCollection'

type Props = {}

const page = (props: Props) => {
  return (
    <div className='flex flex-col gap-8 lg:w-[80%] mx-auto mt-12'>
        <div className='flex flex-col gap-3 border-b pb-5'>
            <TypographyH2 text='Mint a New Product'/>
            <TypographyP text='Once your item is minted you will not be able to change any of its information.'/>   
        </div>
        <DragAndDrop/>
        <form className="flex flex-col gap-5">
            <div className="grid w-full max-w-md items-center gap-1.5">
                <Label htmlFor="name">Name</Label>
                <Input type="name" id="name" placeholder="Name" />
            </div>
            <div className="grid w-full gap-1.5">
                <Label htmlFor="description">Description</Label>
                <Textarea placeholder="Write Description" id="description" className='h-[10rem]' />
                <p className="text-sm text-muted-foreground">
                    The description will be included on the item&apos;s detail page underneath its image. Markdown syntax is supported.
                </p>
            </div>
            <div className="grid w-full max-w-md items-center gap-1.5">
                <Label htmlFor="link">External Link</Label>
                <Input type="link" id="link" placeholder="External Link" />
            </div>
            <div className="grid w-full max-w-md items-center gap-1.5">
                <Label htmlFor="royality">Royality</Label>
                <Input type="royality" id="royality" placeholder="10%" />
            </div>
        </form>
        <ChooseCollection/>
        <div className='flex flex-col gap-5'>
            <div className='flex justify-between items-center'>
                <div>
                    <TypographyH4 text='Put on sale'/>
                    <TypographySmall text='You’ll receive bids on this item'/>
                </div>
                <Switch className='w-12' />
            </div>
            <div className='flex justify-between items-center'>
                <div>
                    <TypographyH4 text='Instant sale price'/>
                    <TypographySmall text='Enter the price for which the item will be instantly sold'/>
                </div>
                <Switch className='w-12'/>
            </div>
            <div className='flex justify-between items-center'>
                <div>
                    <TypographyH4 text='Unlock once purchased'/>
                    <TypographySmall text='Content will be unlocked after successful transaction'/>
                </div>
                <Switch className='w-12'/>
            </div>
        </div>
        <Button className='rounded-full self-end'>Mint Product</Button>
    </div>
  )
}

export default page