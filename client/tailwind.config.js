/** @type {import('tailwindcss').Config} */
export default {
  content: ["./index.html", "./src/**/*.{js,ts,jsx,tsx}"],
  theme: {
    extend: {
      fontFamily: {
        dseg7: ["DSEG7", "mono"],
        dseg14: ["DSEG14", "mono"],
      },
    },
  },
  plugins: [],
};
