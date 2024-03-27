import { TypographyP, TypographySmall } from "../common/Typography";
import { LuImagePlus } from "react-icons/lu";
import Image from "next/image";
import { Input } from "../ui/input";
import { useRef, useState } from "react";
import { Button } from "../ui/button";
import { MdDelete } from "react-icons/md";

type Props = {
  onChange: (a: File | undefined) => void;
};

const ThumbnailUpload = ({ onChange }: Props) => {
  const ref = useRef<HTMLInputElement>(null);
  const [file, setFile] = useState<File>();
  const handleDrop = (e: any) => {
    e.preventDefault();
    const files = e.dataTransfer?.files;
    onChange(files?.[0]);
    setFile(files?.[0]);
  };
  return (
    <div
      onDrop={(e) => handleDrop(e)}
      className="relative z-40 flex flex-col items-center justify-center border-3 border-dashed border-foreground rounded-lg h-[20rem] mt-5 bg-accent hover:bg-accent/70 cursor-pointer"
      onClick={() => ref.current?.click()}
    >
      {!file ? (
        <>
          <LuImagePlus size={50} />
          <TypographyP text="Upload a file or drag and drop" />
          <TypographySmall text="PNG, JPG, GIF up to 10MB" />
          <Input
            ref={ref}
            type="file"
            accept="image/*,video/*"
            className="hidden"
            onChange={(e) => {
              setFile(e.target.files?.[0]);
              onChange(e.target.files?.[0]);
            }}
          />
        </>
      ) : (
        <>
          {file.type.startsWith("image") && (
            <div className="relative rounded-lg mx-auto w-full h-full">
              <Image
                className="rounded-lg  object-contain"
                src={URL.createObjectURL(file)}
                fill
                alt=""
              />
            </div>
          )}
          {file.type.startsWith("video") && (
            <div className="relative mx-auto w-full h-full">
              <video
                className="absolute rounded-lg object-cover"
                autoPlay
                src={URL.createObjectURL(file)}
              />
            </div>
          )}
          <Button
            type="button"
            variant="ghost"
            size="icon"
            className="absolute bg-background/50 hover:bg-background hover right-5 top-5 rounded-full"
            onClick={() => {
              onChange(undefined);
              setFile(undefined);
            }}
          >
            <MdDelete size={25} />
          </Button>
        </>
      )}
    </div>
  );
};

export default ThumbnailUpload;
