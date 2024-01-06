'use client'
import { TypographyH4, TypographyP, TypographySmall } from '@/components/common/Typography'
import ImageUpload from '@/components/mint/ImageUpload'
import { Input } from '@/components/ui/input'
import { Textarea } from '@/components/ui/textarea'
import ChooseCollection from '@/components/mint/ChooseCollection'
import { zodResolver } from "@hookform/resolvers/zod"
import { useForm } from "react-hook-form"
import * as z from "zod"
import {
  Form,
  FormControl,
  FormDescription,
  FormField,
  FormItem,
  FormLabel,
  FormMessage,
} from "@/components/ui/form"
import {
  Dialog,
  DialogContent,
  DialogDescription,
  DialogHeader,
  DialogTitle,
} from "@/components/ui/dialog"

import { IoCheckmarkCircle } from "react-icons/io5";
import { ContractWriteContext } from '@/context/ContractWrite'
import { ToastAction } from '../ui/toast'

import { File } from 'nft.storage'
import { cn, storeAsset } from '@/lib/utils'
import { NFT } from '@/types'
import { CalendarIcon, Loader2 } from "lucide-react"
import { useToast } from "@/components/ui/use-toast"
import { RiAuctionLine } from "react-icons/ri";
import { MdOutlineSell } from "react-icons/md";
import {
  Popover,
  PopoverContent,
  PopoverTrigger,
} from "@/components/ui/popover"
import { Calendar } from "@/components/ui/calendar"
import { format } from 'date-fns'
import { Button } from '../ui/button'
import { useContext, useEffect, useState } from 'react'

interface FormInput {
  name: string;
  description: string;
  image:File | null;
  files: FileList | null;
  royalty:number,
  price:number,
  collection: string;
  auction:boolean;
  auctionEnd:number;
}

const initialState:FormInput = {
  name: '',
  description:'',
  image:null,
  files:null,
  price:0.0,
  auction:false,
  royalty: 0.0,
  collection: '',
  auctionEnd:0.0,
}
const schema = z.object({
  name: z.string().min(3),
  description: z.string().min(5),
  image:z.any().refine((file) => 
   file && file.size <= 20 * 1024 * 1024,
    'File size must be less than 20MB.'
  ),
  files:z.any().refine((files) => 
    files && files.length > 0,
    'Files are required'
  ),
  royalty: z.number().nonnegative().max(10),
  price: z.number().nonnegative(),
  auctionEnd: z.number(),
  collection: z.string(),
  auction:z.boolean(),
});

type Props = {}

const MintForm = (props: Props) => {
  const [ uploading, setUploading ] = useState(false)
  const [ uploadSuccess, setUploadSuccess ] = useState(false)
  const [ open, setOpen ] = useState(false)
  const { toast } = useToast()

  const form = useForm<FormInput>({
    resolver:zodResolver(schema),
    defaultValues:initialState
  })

  const { 
    isLoading, 
    isError,
    transactionSuccess,
    prepareArguments, 
    prepareContractWrite 
  } = useContext(ContractWriteContext)
  
  
  const onSubmit = async (values:FormInput) =>{
    const { image, files, ...others } = values
    try {
      setOpen(true)
      setUploading(true)
      const image_cid = await storeAsset(image ? [image]:null)
      const files_cid = await storeAsset(files)
      setUploadSuccess(true)
      const metadata:NFT = {
        ...others,
        image:`https://nftstorage.link/ipfs/${image_cid}/${image?.name}`,
        files:`ipfs://${files_cid}`,
      }
      prepareContractWrite(true, process.env.NEXT_PUBLIC_LISTING_PRICE as string)
      prepareArguments(
        [
          metadata.name,
          metadata.description,
          metadata.image,
          metadata.files,
          metadata.price,
          metadata.auction,
          metadata.auctionEnd,
          metadata.royalty
        ])
    } catch (error) {
      toast({
        variant: "destructive",
        title: "Uh oh! Something went wrong.",
        description: "There was a problem with uploading your files.",
        action: <ToastAction altText="Try again">Try again</ToastAction>,
      })
      setOpen(false)
      console.log('error', error)
    } finally {
      setUploading(false)
    }
  }

  useEffect(() => {
    if(isError || transactionSuccess){
      setOpen(false)
      if(transactionSuccess){
        form.reset(initialState)
      }
    }
  }, [form, isError, transactionSuccess])


  return (
      <Form {...form}>
        <form onSubmit={form.handleSubmit(onSubmit)} className="flex flex-col gap-5">
            <ImageUpload form={form}/>
            <FormField
              control={form.control}
              name="name"
              render={({ field }) => ( 
                <FormItem>
                  <FormLabel>Name</FormLabel>
                  <FormControl>
                    <Input id="name" placeholder="Name" {...field}/>
                  </FormControl>
                  <FormMessage />
                </FormItem>
              )}
            />
            <FormField
              control={form.control}
              name="description"
              render={({ field }) => (
                <FormItem>
                  <FormLabel>Description</FormLabel>
                  <FormControl>
                    <Textarea placeholder="Write Description" id="description" {...field} className='h-[10rem]' />
                  </FormControl>
                  <FormDescription>
                    The description will be included on the item&apos;s detail page underneath its image. Markdown syntax is supported.
                  </FormDescription>
                  <FormMessage />
                </FormItem>
              )}
            />
            <FormField
              control={form.control}
              name="files"
              render={({ field }) => (
                <FormItem>
                  <FormLabel>Files</FormLabel>
                  <FormControl>
                    <Input type="file" id="files" multiple onChange={(e) => field.onChange(e.target.files)} />
                  </FormControl>
                  <FormDescription>
                    Upload Files relevant to the Digital Product you want to mint as NFTs
                  </FormDescription>
                  <FormMessage />
                </FormItem>
              )}
            />
            <FormField
              control={form.control}
              name="auction"
              render={({ field }) => (
                <FormItem >
                  <FormLabel className="text-base">
                    Sale Type
                  </FormLabel>
                  <FormControl>
                    <div className='flex gap-3 items-center'>
                      <Button type="button" variant={!field.value ?'default':'secondary'} className="flex-1 flex gap-3 item-center border rounded-md  h-auto w-[15rem]" onClick={() => field.onChange(false)}>
                        <MdOutlineSell size={30}/>
                        <TypographyP className="text-foregghostround" text='Fixed'/>
                      </Button>
                      <Button type="button" variant={field.value ?'default':'secondary'} className="flex-1 flex gap-3 item-center border rounded-md  h-auto w-[15rem]" onClick={() => field.onChange(true)}>
                        <RiAuctionLine size={30}/>
                        <TypographyP className="text-foreground" text='Auction'/>
                      </Button>
                    </div>
                  </FormControl>
                  <FormDescription>
                    <TypographySmall text='You’ll receive bids on this item'/>
                  </FormDescription>
                </FormItem>
              )}
            />
          {form.getValues('auction') && 
            <FormField
              control={form.control}
              name="auctionEnd"
              render={({ field }) => (
                <FormItem>
                  <FormLabel>Auction End</FormLabel>
                  <FormControl>
                    <Popover>
                      <PopoverTrigger asChild>
                        <Button
                          variant={"secondary"}
                          className={cn(
                            "w-full justify-start text-left font-normal",
                            !field.value && "text-muted-foreground"
                          )}
                        >
                          <CalendarIcon className="mr-2 h-4 w-4" />
                          {field.value ? format(field.value, "PPP") : <span>Auction End Date</span>}
                        </Button>
                      </PopoverTrigger>
                      <PopoverContent className="w-auto p-0">
                        <Calendar
                          mode="single"
                          selected={new Date(field.value)}
                          onSelect={(d) => field.onChange(d?.getTime())}
                          initialFocus
                        />
                      </PopoverContent>
                    </Popover>
                  </FormControl>
                  <FormMessage />
                </FormItem>
              )}
            />}
            <FormField
              control={form.control}
              name="price"
              render={({ field }) => (
                <FormItem>
                  <FormLabel>Price(ETH)</FormLabel>
                  <FormControl>
                    <Input 
                      id="price" 
                      type='number'  
                      placeholder='Enter Price (ETH)'
                      {...field} 
                      value={field.value > 0 ? field.value:undefined} 
                      onChange={(e) => field.onChange(parseFloat(e.target.value))}
                      />
                  </FormControl>
                  <FormDescription>
                    This is Price will be used as a floor price or fixed price
                  </FormDescription>
                  <FormMessage />
                </FormItem>
              )}
            />
            <FormField
              control={form.control}
              name="royalty"
              render={({ field }) => (
                <FormItem>
                  <FormLabel>Royality(%)</FormLabel>
                  <FormControl>
                    <Input 
                      id="royalty" 
                      type='number'  
                      placeholder='Enter Royalty Percentage'
                      {...field} 
                      value={field.value > 0 ? field.value:undefined} 
                      onChange={(e) => field.onChange(parseFloat(e.target.value))}
                      />
                  </FormControl>
                  <FormDescription>
                    This is Royalty you would get on secondary resale of your NFT. 
                  </FormDescription>
                  <FormMessage />
                </FormItem>
              )}
            />
          <ChooseCollection/>
          <Button type='submit' disabled={uploading || isLoading } className='rounded-full self-end' size='lg'>
            {(uploading || isLoading) ? 
            <>
              <Loader2 className="mr-2 h-6 w-6 animate-spin" />
              Minting
            </>:'Mint Product'}
          </Button>
        </form>
        <MintingProgress 
          open={open}
          uploading={uploading} 
          uploadSuccess={uploadSuccess}
          />
      </Form>


  )
}

export default MintForm


type ProgressProps = {
  open:boolean,
  uploading:boolean,
  uploadSuccess:boolean,
}

export const MintingProgress = ({ open, uploading, uploadSuccess }: ProgressProps) => {
  const { 
    waitingForTransaction, 
    transactionSuccess, 
    writing, 
    writeSuccess
   } = useContext(ContractWriteContext)
  return (
      <Dialog open={open}>
        <DialogContent>
          <DialogHeader>
            <DialogTitle>Minting Your Assets</DialogTitle>
            <DialogDescription className='flex flex-col gap-5 pt-10'>
              <div className='flex items-center gap-5'>
                <div className='flex justify-center items-center rounded-full bg-secondary p-2 h-[3.5rem] w-[3.5rem]'>
                  {uploading ? <Loader2 className="h-8 w-8 animate-spin" />:uploadSuccess ? <IoCheckmarkCircle size={40}/>: <TypographyH4 text='1'/>}
                </div>
                <div className='flex flex-col'>
                  <TypographyH4 text='Uploading Files to Decentralized Storage'/>
                  <TypographySmall text='This might take a few minutes'/>
                </div>
              </div>
              <div className='flex items-center gap-5'>
                <div className='flex justify-center items-center rounded-full bg-secondary p-2 h-[3.5rem] w-[3.5rem]'>
                  {writing ? <Loader2 className="h-8 w-8 animate-spin" />: writeSuccess ? <IoCheckmarkCircle size={40}/>:<TypographyH4 text='2'/>}
                </div>
                <div className='flex flex-col'>
                  <TypographyH4 text='Go to your wallet to approve this transaction'/>
                  <TypographySmall text='A blockchain transaction is required to mint your NFT.'/>
                </div>
              </div>
              <div className='flex items-center gap-5'>
                <div className='flex justify-center items-center rounded-full bg-secondary p-2 h-[3.5rem] w-[3.5rem]'>
                  {waitingForTransaction ? <Loader2 className="h-8 w-8 animate-spin" />: transactionSuccess ? <IoCheckmarkCircle size={40}/>:<TypographyH4 text='3'/>}
                </div>
                <div className='flex flex-col'>
                  <TypographyH4 text='Minting Your Assets'/>
                  <TypographySmall text='Please stay on this page and keep this browser tab open.'/>
                </div>
              </div>
            </DialogDescription>
          </DialogHeader>
        </DialogContent>
      </Dialog>
  )
}





























