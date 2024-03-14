import Image from "next/image";
import { TypographyH1, TypographyH4 } from "../common/Typography";
import { Input } from "../ui/input";
import { Button } from "../ui/button";

type Props = {};

const SubscribeSection = (props: Props) => {
  return (
    <div className="flex flex-col-reverse lg:flex-row justify-evenly items-center min-h-[70vh] h-full">
      <div className="flex-1 flex flex-col gap-10 p-10">
        <TypographyH1 text="Join Our Weekly Digest" />
        <TypographyH4 text="Subscribe to our drop list and Get exclusive promotions & updates straight to your inbox." />
        <div className="flex gap-3 items-center lg:w-[80%]">
          <Input
            type="email"
            className="bg-accent text-accent-foreground rounded-lg px-6 h-12"
            placeholder="Enter your Email Address"
          />
          <Button className="rounded-full">Subscribe</Button>
        </div>
      </div>
      <div className="flex-1 flex justify-center">
        <Image
          className="rounded-2xl"
          src="/landing-page/subscribe.png"
          width={800}
          height={501}
          alt=""
        />
      </div>
    </div>
  );
};

export default SubscribeSection;
