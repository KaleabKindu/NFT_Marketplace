"use client";
import { Avatar } from "../common/Avatar";
import {
  TypographyH4,
  TypographyP,
  TypographySmall,
} from "../common/Typography";
import { ScrollArea, ScrollBar } from "../ui/scroll-area";
import { IoCheckmarkCircle } from "react-icons/io5";
import { Button } from "../ui/button";
import { MdAdd } from "react-icons/md";
import { cn } from "@/lib/utils";
type Props = {};

const ChooseCollection = (props: Props) => {
  return (
    <div>
      <TypographyH4 text="Add To a Collection(Optional)" />
      <TypographySmall text="Choose an exiting collection or create a new one" />
      <ScrollArea>
        <div className="flex gap-5 mt-5">
          <Button
            type="button"
            variant={"ghost"}
            className="flex flex-col gap-5 p-8 border whitespace-normal text-left items-center rounded-md  h-auto w-[15rem]"
          >
            <MdAdd size={30} />
          </Button>
        </div>
        <ScrollBar className="hidden" orientation="horizontal" />
      </ScrollArea>
    </div>
  );
};

type CollectionProps = {
  selected: boolean;
};

const Collection = ({ selected }: CollectionProps) => {
  return (
    <Button
      type="button"
      variant={selected ? "default" : "ghost"}
      className="flex flex-col gap-5 p-5 border whitespace-normal text-left items-stretch rounded-md  h-auto w-[15rem]"
    >
      <div className="flex justify-between items-center">
        <Avatar className="w-10 h-10" />
        {selected && <IoCheckmarkCircle size={30} />}
      </div>
      <TypographyP
        className={cn({ "text-foreground": selected })}
        text="Crypto Legend Professor"
      />
    </Button>
  );
};

export default ChooseCollection;
