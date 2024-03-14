import Image from "next/image";
import { Card } from "./ui/card";
import {
  TypographyH3,
  TypographyH4,
  TypographySmall,
} from "./common/Typography";
import { Badge } from "./ui/badge";
import { Avatar } from "./common/Avatar";
import Link from "next/link";
import { Routes } from "@/routes";
type Props = {};

const CollectionCard = (props: Props) => {
  return (
    <Link
      href={`${Routes.COLLECTION}/${Math.floor(Math.random() * 10000000000)}`}
    >
      <Card className="flex flex-col gap-2 bg-accent hover:bg-accent/70 max-w-[35rem] h-[35rem] w-full group">
        <div className="relative overflow-clip h-[55%] ">
          <Image
            className="object-cover rounded-t-lg group-hover:scale-105"
            src="/landing-page/art-category.jpg"
            fill
            alt=""
          />
        </div>
        <div className="flex gap-2 h-[20%]">
          <div className="relative flex-1">
            <Image
              className="object-cover hover:scale-105"
              src="/landing-page/ebook-category.png"
              fill
              alt=""
            />
          </div>
          <div className="relative flex-1">
            <Image
              className="object-cover hover:scale-105"
              src="/landing-page/drawing-category.jpg"
              fill
              alt=""
            />
          </div>
          <div className="relative flex-1">
            <Image
              className="object-cover hover:scale-105"
              src="/landing-page/3d-category.jpg"
              fill
              alt=""
            />
          </div>
        </div>
        <div className="flex justify-between items-center p-5">
          <div className="flex flex-col gap-3">
            <TypographyH3
              className="text-primary/60"
              text="Collection #12232"
            />
            <div className="flex items-center gap-5">
              <Avatar className="h-8 w-8" />
              <TypographySmall text="Tony Stark" />
            </div>
          </div>
          <Badge variant="outline" className="self-start border-2 rounded-md">
            <TypographyH4 className="text-primary/60" text="0.0136ETH" />
          </Badge>
        </div>
      </Card>
    </Link>
  );
};

export default CollectionCard;
