"use client";
import { useState, useRef, useEffect } from "react";
import { FaPlay, FaPause } from "react-icons/fa6";
import { Button } from "@/components/ui/button";
import clsx from "clsx";

type Props = {
  url: string;
  className?: string;
};

const VideoPlayer = ({ url, className }: Props) => {
  const [playing, setPlaying] = useState(false);
  const videoRef = useRef<HTMLVideoElement>(null);

  useEffect(() => {
    const handlePlaying = () => setPlaying(true);
    const handlePause = () => setPlaying(false);
    videoRef.current?.addEventListener("play", handlePlaying);
    videoRef.current?.addEventListener("pause", handlePause);

    return () => {
      videoRef.current?.removeEventListener("play", handlePlaying);
      videoRef.current?.removeEventListener("pause", handlePause);
    };
  }, []);

  return (
    <>
      <video
        ref={videoRef}
        src={url}
        className={clsx("absolute w-full h-full object-cover", className)}
        controls={false}
      />
      <div className="absolute flex justify-between bottom-2 left-2">
        <Button
          variant={"ghost"}
          className="rounded-full h-16 w-16 bg-background/30"
          onClick={(e) => {
            e.preventDefault();
            if (playing) {
              videoRef.current?.pause();
            } else {
              videoRef.current?.play();
            }
          }}
        >
          {playing ? <FaPause size={30} /> : <FaPlay size={30} />}
        </Button>
      </div>
    </>
  );
};

export default VideoPlayer;
