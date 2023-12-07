import type { Config } from 'tailwindcss'


const withMT = require("@material-tailwind/react/utils/withMT");

const config: Config = withMT({
  content: [
    './src/pages/**/*.{js,ts,jsx,tsx,mdx}',
    './src/components/**/*.{js,ts,jsx,tsx,mdx}',
    './src/app/**/*.{js,ts,jsx,tsx,mdx}',
  ],
  theme: {
    extend: {
      backgroundImage: {
        'gradient-radial': 'radial-gradient(var(--tw-gradient-stops))',
        'gradient-conic':
          'conic-gradient(from 180deg at 50% 50%, var(--tw-gradient-stops))',
      },
      colors:{
        'primary':'#1d4ed8',
        'dark-bg':'#111827',
        'dark-text':'#ffffff',
        'dark-hover-bg':'#374151',
        'dark-card':'#1f2937',
        'dark-border':'#374151'
      }
    },
  },
  plugins: [],
})
export default config
