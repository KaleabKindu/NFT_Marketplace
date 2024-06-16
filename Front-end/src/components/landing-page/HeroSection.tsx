"use client"
import Image from "next/image";
import { Button } from "../ui/button";
import { TypographyH1, TypographyH3 } from "../common/Typography";
import { useAppSelector } from "@/store/hooks";
import { useWeb3Modal } from "@web3modal/wagmi/react";

type Props = {};

const HeroSection = (props: Props) => {
  const session = useAppSelector(state => state.auth.session)
  const { open } = useWeb3Modal()
  return (
    <div className="flex flex-col-reverse lg:flex-row justify-center items-center min-h-[70vh] h-full">
      <div className="flex-1 flex flex-col gap-5 p-3 lg:p-8">
        <TypographyH1 text="Discover Digital Products & Trade as NFTs" />
        <TypographyH3 text="Trade unique digital products as NFTs on NFT Marketplace, the innovative platform empowering creators and collectors." />
        <div className="text-3xl"></div>
        {!session && 
        <Button className="self-start" size={"lg"} onClick={() => open()}>
          Get Started
        </Button>}
      </div>
      <div className="flex-1">
        <Image
          className="object-cover"
          src="/landing-page/hero-image.png"
          width={1000}
          height={501}
          alt=""
        />
      </div>
    </div>
  );
};

export default HeroSection;
