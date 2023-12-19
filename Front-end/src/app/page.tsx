import HeroSection from "@/components/landing-page/HeroSection";
import HowItWorkSection from "@/components/landing-page/HowItWorkSection";

export default function Home() {
  return (
    <div className="flex flex-col">
      <HeroSection/>
      <HowItWorkSection/>
    </div>
  )
}
