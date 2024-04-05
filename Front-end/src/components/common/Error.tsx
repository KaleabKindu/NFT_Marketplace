import { MdErrorOutline } from "react-icons/md";
import { TypographySmall } from "./Typography";

type Props = {
  message?: string;
};

const Error = ({ message = "Something went wrong!!!" }: Props) => {
  return (
    <div className="col-span-12 w-full h-72 flex flex-col gap-3 items-center justify-center">
      <MdErrorOutline className="text-red-500" size={60} />
      <TypographySmall text={message} />
    </div>
  );
};

export default Error;
