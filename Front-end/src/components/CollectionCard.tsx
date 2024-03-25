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
import { Collection } from "@/types";
type Props = {
  collection: Collection;
};

const CollectionCard = ({ collection }: Props) => {
  return (
    <Link href={`${Routes.COLLECTION}/${collection.id}`}>
      <Card className="flex flex-col gap-2 rounded-3xl bg-accent hover:bg-accent/70  h-[25rem] w-full group">
        <div className="relative overflow-clip rounded-t-3xl h-[55%] ">
          <Image
            className="object-cover rounded-t-3xl group-hover:scale-105"
            src={collection?.images?.[0] || ""}
            fill
            alt=""
          />
        </div>
        <div className="flex gap-2 h-[20%]">
          <div className="relative flex-1">
            <Image
              className="object-cover hover:scale-105"
              src={collection?.images?.[1] || ""}
              fill
              alt=""
            />
          </div>
          <div className="relative flex-1">
            <Image
              className="object-cover hover:scale-105"
              src={collection?.images?.[2] || ""}
              fill
              alt=""
            />
          </div>
          <div className="relative flex-1">
            <Image
              className="object-cover hover:scale-105"
              src={collection?.images?.[3] || ""}
              fill
              alt=""
            />
          </div>
        </div>
        <div className="flex flex-col gap-3 p-5">
          <TypographyH3
            className="text-primary/60 capitalize"
            text={collection.name}
          />
          <div className="flex justify-between">
            <div className="flex items-center gap-3">
              <Avatar className="h-8 w-8" src={collection.creator.avatar} />
              <TypographySmall text={collection.creator.username} />
            </div>
            <Card className="relative p-2 bg-primary/5 border-4">
              <Badge variant={"secondary"} className="absolute -top-3 left-1">
                Volume
              </Badge>
              <TypographyH4
                className="text-primary/60"
                text={`${collection.volume}ETH`}
              />
            </Card>
          </div>
        </div>
      </Card>
    </Link>
  );
};

export default CollectionCard;
