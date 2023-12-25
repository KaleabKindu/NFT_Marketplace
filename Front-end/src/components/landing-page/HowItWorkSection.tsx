import { howitworks } from '@/data'
import Image from 'next/image'
import { Badge } from "@/components/ui/badge"

type Props = {}

const HowItWorkSection = (props: Props) => {
  return (
    <div className='grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-4 gap-5 my-16'>
        {
            howitworks.map((step, index) => 
            <div key={index} className='flex flex-col text-center items-center gap-8'>
                <Image src={`/landing-page/${step.image}`} width={100} height={100} alt={`step ${step.index}'s image`} />
                <Badge variant={'secondary'} className='text-md'>Step {step.index}</Badge>
                <div className='scroll-m-20 text-2xl font-semibold tracking-tight'>{step.name}</div>
                <div>{step.description}</div>
            </div>
            )
        }
        
    </div>
  )
}

export default HowItWorkSection