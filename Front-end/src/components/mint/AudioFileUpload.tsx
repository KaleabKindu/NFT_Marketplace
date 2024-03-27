import { TypographyP, TypographySmall } from "../common/Typography";
import { LuFileAudio2 } from "react-icons/lu";
import { Input } from "../ui/input";
import { useRef, useState } from "react";
import { Button } from "../ui/button";
import { MdDelete } from "react-icons/md";
// @ts-ignore
import { AudioVisualizer } from "react-audio-visualize";

type Props = {
  onChange: (a: File | undefined) => void;
};

const AudioFileUpload = ({ onChange }: Props) => {
  const ref = useRef<HTMLInputElement>(null);
  const [blob, setBlob] = useState<Blob>();
  const handleDrop = (e: any) => {
    e.preventDefault();
    const files = e.dataTransfer?.files;
    onChange(files?.[0]);
    setBlob(files?.[0]);
  };
  return (
    <div
      onDrop={(e) => handleDrop(e)}
      className="relative z-40 flex flex-col items-center justify-center border-3 border-dashed border-foreground rounded-lg h-[7rem] mt-5 bg-accent hover:bg-accent/70 cursor-pointer"
      onClick={() => ref.current?.click()}
    >
      {!blob ? (
        <div className="flex gap-10 items-center">
          <LuFileAudio2 size={50} />
          <div className="flex flex-col items-center justify-center gap-1">
            <TypographyP text="Upload audio file or drag'n drop" />
            <TypographySmall
              className="text-center"
              text="MP3, WAV, OGG up to 10MB"
            />
          </div>
          <Input
            ref={ref}
            type="file"
            accept="audio/*"
            className="hidden"
            onChange={(e) => {
              console.log("here");
              setBlob(e.target.files?.[0]);
              onChange(e.target.files?.[0] as File);
            }}
          />
        </div>
      ) : (
        <>
          <AudioVisualizer
            blob={blob}
            src
            width={500}
            height={75}
            barWidth={1}
            gap={3}
          />

          <Button
            type="button"
            variant="ghost"
            size="icon"
            className="absolute bg-background/50 hover:bg-background right-3 top-3 rounded-full"
            onClick={() => {
              onChange(undefined);
              setBlob(undefined);
            }}
          >
            <MdDelete size={25} />
          </Button>
        </>
      )}
    </div>
  );
};

export default AudioFileUpload;
