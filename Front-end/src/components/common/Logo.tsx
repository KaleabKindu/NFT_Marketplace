import Image from "next/image";
import Link from "next/link";

type Props = {};

const Logo = (props: Props) => {
  return (
    <Link href="/" className="flex gap-2 items-center">
      <Image
        className="w-[80px] h-[60px] lg:w-[100px] lg:h-[70px]"
        src="/logo.png"
        width={150}
        height={150}
        alt="NFT Marketplace's Logo"
      />
      <div className="text-lg lg:text-2xl font-bold">NFT Gebya</div>
    </Link>
  );
};

export default Logo;
