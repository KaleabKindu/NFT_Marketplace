"use client";
import { useState, useEffect } from "react";
import { Avatar } from "../common/Avatar";
import { TypographyP } from "../common/Typography";
import { ScrollArea, ScrollBar } from "../ui/scroll-area";
import { IoCheckmarkCircle } from "react-icons/io5";
import { Button } from "../ui/button";
import { cn } from "@/lib/utils";
import { ICollection } from "@/types";
import AddCollectionModal from "./AddCollectionModal";
import { useGetCollectionsQuery } from "@/store/api";
import { useAccount } from "wagmi";

type Props = {
  onChange: (a: string) => void;
};

const ChooseCollection = ({ onChange }: Props) => {
  const { address } = useAccount();
  const [selected, setSelected] = useState("");
  const [collections, setCollections] = useState<ICollection[]>([]);
  const { data } = useGetCollectionsQuery({
    creator: address,
  });
  const handleClick = (id: string) => {
    if (id === selected) {
      setSelected("");
      onChange("");
    } else {
      setSelected(id);
      onChange(id);
    }
  };
  useEffect(() => {
    if (data) {
      setCollections(data.value);
    }
  }, [data]);

  return (
    <div>
      <ScrollArea>
        <div className="flex gap-5">
          <AddCollectionModal />
          {collections.map((collection, index) => (
            <Collection
              key={index}
              collection={collection}
              selected={selected === collection.id}
              onSelected={handleClick}
            />
          ))}
        </div>
        <ScrollBar className="hidden" orientation="horizontal" />
      </ScrollArea>
    </div>
  );
};

type CollectionProps = {
  collection: ICollection;
  selected: boolean;
  onSelected: (a: string) => void;
};

const Collection = ({ collection, onSelected, selected }: CollectionProps) => {
  return (
    <Button
      type="button"
      variant={selected ? "default" : "ghost"}
      onClick={() => onSelected(collection.id)}
      className="flex flex-col gap-5 p-5 border whitespace-nowrap text-left items-stretch rounded-2xl h-auto w-[200px]"
    >
      <div className="flex justify-between">
        <Avatar src={collection.avatar} className="w-24 h-24 rounded-2xl" />
        {selected && <IoCheckmarkCircle size={40} />}
      </div>
      <TypographyP
        className={cn("whitespace-nowrap text-ellipsis overflow-hidden", {
          "text-foreground": selected,
        })}
        text={collection.name}
      />
    </Button>
  );
};

export default ChooseCollection;
