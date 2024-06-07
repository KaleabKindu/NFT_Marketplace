"use client";
import { useState, useRef, useEffect } from "react";
import { FaPlay, FaPause } from "react-icons/fa6";
// @ts-ignore
import { AudioVisualizer } from "react-audio-visualize";
import { Button } from "@/components/ui/button";

type Props = {
  url: string;
  width?: number;
};

const AudioPlayer = ({ url, width }: Props) => {
  const [audioWidth, setAudioWidth] = useState(250);
  const buttonRef = useRef<HTMLButtonElement>(null);
  const [playing, setPlaying] = useState(false);
  const [currentTime, setCurrentTime] = useState(0);
  const [blob, setBlob] = useState<Blob | null>(null);
  const audioRef = useRef<HTMLAudioElement>(null);

  useEffect(() => {
    const fetchAudioAsBlob = async (audioUrl: string) => {
      try {
        const response = await fetch(audioUrl);
        if (!response.ok) {
          throw new Error("Failed to fetch audio");
        }

        const blob = await response.blob();

        setBlob(blob);
      } catch (error) {
        console.error("Error fetching audio:", error);
        return null;
      }
    };
    if (audioRef.current) {
      fetchAudioAsBlob(url);
    }
  }, [audioRef.current]);
  useEffect(() => {
    const handlePlaying = () => setPlaying(true);
    const handlePause = () => setPlaying(false);
    const handleEnded = () => {
      setPlaying(false);
      setCurrentTime(0);
    };
    audioRef.current?.addEventListener("play", handlePlaying);
    audioRef.current?.addEventListener("pause", handlePause);
    audioRef.current?.addEventListener("ended", handleEnded);

    return () => {
      audioRef.current?.removeEventListener("play", handlePlaying);
      audioRef.current?.removeEventListener("pause", handlePause);
      audioRef.current?.removeEventListener("ended", handleEnded);
    };
  }, []);
  useEffect(() => {
    if (width && buttonRef.current) {
      setAudioWidth(width - buttonRef.current.offsetWidth - 15);
    }
  }, [width, buttonRef]);
  console.log("audio", width, audioWidth);
  return (
    <div className="absolute flex bg-background/30 rounded-full justify-between items-center bottom-1 left-1/2 -translate-x-1/2 px-2">
      <AudioVisualizer
        blob={blob}
        src
        width={audioWidth}
        height={75}
        currentTime={currentTime}
        barWidth={1}
        barColor={"#FFFFFF"}
        barPlayedColor={"#3358eb"}
        gap={3}
      />
      <audio
        ref={audioRef}
        className="hidden"
        crossOrigin="anonymous"
        src={url}
        onTimeUpdate={(a) => setCurrentTime(a.currentTarget.currentTime)}
      />
      <Button
        ref={buttonRef}
        variant={"ghost"}
        className="rounded-full h-16 w-16 bg-background/30"
        onClick={(e) => {
          e.preventDefault();
          if (playing) {
            audioRef.current?.pause();
          } else {
            audioRef.current?.play();
          }
        }}
      >
        {playing ? <FaPause size={25} /> : <FaPlay size={25} />}
      </Button>
    </div>
  );
};

export default AudioPlayer;
