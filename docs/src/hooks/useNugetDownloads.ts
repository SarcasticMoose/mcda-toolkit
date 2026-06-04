import { useEffect, useState } from 'react';

const CACHE_TTL = 5 * 60 * 1000;

export function useNugetDownloads(packageId: string): string {
  const cacheKey = `nuget_downloads_${packageId}`;
  const [downloads, setDownloads] = useState<string>(() => {
    try {
      const cached = localStorage.getItem(cacheKey);
      if (cached) {
        const { value, ts } = JSON.parse(cached) as { value: string; ts: number };
        if (Date.now() - ts < CACHE_TTL) return value;
      }
    } catch {}
    return '';
  });

  useEffect(() => {
    try {
      const cached = localStorage.getItem(cacheKey);
      if (cached) {
        const { ts } = JSON.parse(cached) as { value: string; ts: number };
        if (Date.now() - ts < CACHE_TTL) return;
      }
    } catch {}

    fetch(`https://azuresearch-usnc.nuget.org/query?q=packageid:${packageId}&prerelease=true`)
      .then(res => res.json())
      .then((data: { data: { totalDownloads?: number }[] }) => {
        const total = data.data?.[0]?.totalDownloads;
        if (total !== undefined) {
          const value = formatDownloads(total);
          setDownloads(value);
          try {
            localStorage.setItem(cacheKey, JSON.stringify({ value, ts: Date.now() }));
          } catch {}
        }
      })
      .catch(() => {});
  }, [packageId]);

  return downloads;
}

function formatDownloads(n: number): string {
  if (n >= 1_000_000) return `${(n / 1_000_000).toFixed(1)}M`;
  if (n >= 1_000) return `${(n / 1_000).toFixed(1)}k`;
  return String(n);
}
