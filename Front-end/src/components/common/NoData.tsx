import { SlSocialDropbox } from "react-icons/sl";
import { TypographySmall } from "./Typography";

type Props = {
    message?:string
}

const NoData = ({ message = 'No data found'}: Props) => {
  return (
    <div className='col-span-12 w-full h-72 flex flex-col gap-3 items-center justify-center'>
       <SlSocialDropbox size={60} /> 
       <TypographySmall text={message} />
    </div>
  )
}

export default NoData