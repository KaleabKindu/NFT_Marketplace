import BrowseCategorySection from "@/components/landing-page/BrowseCategorySection";
import HeroSection from "@/components/landing-page/HeroSection";
import HowItWorkSection from "@/components/landing-page/HowItWorkSection";

export default function Home() {
  return (
    <div className="flex flex-col gap-8 lg:gap-16">
      <HeroSection/>
      <HowItWorkSection/>
      <BrowseCategorySection/>
    </div>
  )
}
