import "@/util/dateTools";
import Navbar from "../navbar/navbar";

export default function Home() {
  let forecast: Weather[] = getForecast();

  return (
    <>
      <Navbar />
      <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-5 gap-4 p-4">
        {forecast.map((day, index) => (
          <div
            key={index}
            className="bg-white rounded-lg shadow-md p-6 border border-gray-200"
          >
            <h3 className="text-lg font-semibold text-gray-800 mb-2">
              {new Date(day.date).toLocaleDateString('en-US', {
                weekday: 'short',
                month: 'short',
                day: 'numeric'
              })}
            </h3>
            <div className="text-4xl font-bold text-blue-600 mb-2">
              {day.temperature}°C
            </div>
            <p className="text-gray-600 capitalize">
              {day.summary}
            </p>
          </div>
        ))}
      </div>
    </>
  );
}

const summaries: string[] = [
  "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
] as const;

interface Weather {
  date: string;
  temperature: number;
  summary: string;
}

function getForecast(): Weather[] {
  return Array.from({ length: 5 }, (_, index) => ({
    date: new Date().addDays(index + 1).toISOString().split('T')[0],
    temperature: Math.floor(Math.random() * 75) - 20, // Random between -20 and 54
    summary: summaries[Math.floor(Math.random() * summaries.length)]
  }));
}