import Image from "next/image";
import { TypographyH2, TypographyP } from "../common/Typography";
import { TbCopy } from "react-icons/tb";
import { Button } from "../ui/button";
import { BsThreeDots } from "react-icons/bs";
import {
  DropdownMenu,
  DropdownMenuContent,
  DropdownMenuItem,
  DropdownMenuTrigger,
} from "@/components/ui/dropdown-menu";
import { BsTwitterX, BsFacebook, BsTelegram, BsYoutube } from "react-icons/bs";
import { users } from "@/utils";
import Link from "next/link";
import CopyToClipboard from "react-copy-to-clipboard";
import { Badge } from "../ui/badge";
import { useToast } from "../ui/use-toast";
import { useAccount } from "wagmi";
import { MdEdit } from "react-icons/md";

type Props = {
  address: string;
};

const UserDetail = ({ address }: Props) => {
  const { toast } = useToast();
  const account = useAccount();
  const user = users.find((user) => user.publicAddress === address);
  return (
    <div className="relative flex flex-col lg:flex-row gap-8 -mt-[15vh] w-[90%] lg:w-[85%] mx-auto bg-background border z-40 rounded-3xl p-8">
      <div className="relative w-full h-[200px] lg:w-[300px] lg:h-[250px]">
        <Image
          className="rounded-3xl object-cover"
          src={user?.avatar || "/collection/collection-pic.png"}
          fill
          alt=""
        />
      </div>
      <div className="flex flex-col items-start gap-5">
        <TypographyH2 text={user?.username || "Anthony Stark"} />
        <CopyToClipboard
          text={user?.publicAddress || ""}
          onCopy={() =>
            toast({
              title: "Copied to Clipboard",
            })
          }
        >
          <Badge
            variant={"secondary"}
            className="flex gap-2 items-center cursor-pointer"
          >
            <TypographyP
              className="truncate text-right select-none"
              text={user?.publicAddress}
            />
            <TbCopy size={20} />
          </Badge>
        </CopyToClipboard>
        <TypographyP
          text={
            user?.bio ||
            "Punk #4786 / An OG Cryptopunk Collector, hoarder of NFTs. Contributing to @ether_cards, an NFT Monetization Platform."
          }
        />
        <div className="flex gap-5">
          <Link href={user?.social_media?.facebook || ""}>
            <Button className="rounded-full" variant={"ghost"} size={"icon"}>
              <BsFacebook size={25} />
            </Button>
          </Link>
          <Link href={user?.social_media?.twitter || ""}>
            <Button className="rounded-full" variant={"ghost"} size={"icon"}>
              <BsTwitterX size={25} />
            </Button>
          </Link>
          <Link href={user?.social_media?.youtube || ""}>
            <Button className="rounded-full" variant={"ghost"} size={"icon"}>
              <BsYoutube size={25} />
            </Button>
          </Link>
          <Link href={user?.social_media?.telegram || ""}>
            <Button className="rounded-full" variant={"ghost"} size={"icon"}>
              <BsTelegram size={25} />
            </Button>
          </Link>
        </div>
      </div>
      <div className="self-start flex items-center gap-5 ml-auto">
        {account.address === address ? (
          <Link href="">
            <Button variant={"ghost"} size={"icon"} className="rounded-full">
              <MdEdit size={30} />
            </Button>
          </Link>
        ) : (
          <Button className="rounded-full">Follow</Button>
        )}
      </div>
    </div>
  );
};

export default UserDetail;
