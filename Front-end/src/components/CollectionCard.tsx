"use client";
import Image, { ImageProps } from "next/image";
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
import { ICollection } from "@/types";
import CustomImage from "./common/CustomImage";
type Props = {
  collection: ICollection;
};

const CollectionCard = ({ collection }: Props) => {
  return (
    <Link
      href={`${Routes.COLLECTION}/${collection.id}`}
      className="col-span-1 md:col-span-2 lg:col-span-3"
    >
      <Card className="flex flex-col gap-2 rounded-3xl bg-accent hover:bg-accent/70  h-[25rem] w-full group">
        <div className="relative overflow-clip rounded-t-3xl h-[75%] ">
          <CustomImage
            className="object-cover rounded-t-3xl group-hover:scale-105"
            src={collection?.images?.[0] || ""}
            fill
            alt=""
          />
        </div>
        <div className="flex gap-2 h-[20%]">
          <div className="relative flex-1">
            <CustomImage
              className="object-cover hover:scale-105"
              src={collection?.images?.[1] || ""}
              fill
              alt=""
            />
          </div>
          <div className="relative flex-1">
            <CustomImage
              className="object-cover hover:scale-105"
              src={collection?.images?.[2] || ""}
              fill
              alt=""
            />
          </div>
          <div className="relative flex-1">
            <CustomImage
              className="object-cover hover:scale-105"
              src={collection?.images?.[3] || ""}
              fill
              alt=""
            />
          </div>
        </div>
        <div className="flex flex-col gap-3 px-5 py-2">
          <TypographyH3
            className="whitespace-nowrap text-ellipsis overflow-hidden text-primary/60 capitalize"
            text={collection.name}
          />
          <div className="flex justify-between">
            <div className="flex items-center gap-3">
              <Avatar className="h-8 w-8" src={collection.creator.avatar} />
              <TypographySmall
                className="whitespace-nowrap text-ellipsis overflow-hidden"
                text={collection.creator.userName}
              />
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
