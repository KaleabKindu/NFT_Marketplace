import { categories } from '@/data'
import Image from 'next/image' 
import {
  Card,
  CardContent,
  CardFooter,
} from "@/components/ui/card"
import { TypographyH2, TypographyH4, TypographySmall } from '../common/Typography'
import Link from 'next/link'
import { Routes } from '@/routes'


type Props = {}

const BrowseCategorySection = (props: Props) => {
  return (
    <div className='flex flex-col gap-5'>
      <TypographyH2 text={'Browse By Category'}/>
      <div className='grid grid-cols-1 xs:grid-cols-2 md:grid-cols-3 lg:grid-cols-5 gap-5'>
        {
          categories.map((category, index) => 
          <Link key={index} href={Routes.NFTS}>
            <Card className='hover:scale-110'>
              <CardContent className='relative h-44 p-0'>
                <Image className=' object-cover rounded-t-lg' src={`/landing-page/${category.image}`} fill alt=''/>
              </CardContent>
              <CardFooter className='flex flex-col items-start gap-2 px-5 py-2 '>
                <TypographyH4 className='text-primary/80' text={category.name}/>
                <TypographySmall className='text-primary/50' text={`${category.count} NFTs`}/>
              </CardFooter>
            </Card>
          </Link>

          )
        }
      </div>
    </div>
  )
}

export default BrowseCategorySection