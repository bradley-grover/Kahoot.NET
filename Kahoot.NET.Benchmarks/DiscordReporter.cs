using System.Globalization;
using System.Reflection;
using System.Text;
using BenchmarkDotNet.Extensions;
using BenchmarkDotNet.Reports;
using Discord;
using Discord.Webhook;
using Humanizer;

namespace Kahoot.NET.Benchmarks;

public static class DiscordReporter
{
#nullable disable
    internal static DiscordWebhookClient _client;
#nullable restore

    public static void CreateClient(string webHookUrl)
    {
        _client = new(webHookUrl);
    }

    public static async Task SendMessageAsync(Summary summary, Type benchmarkType)
    {
        var attr = benchmarkType.GetCustomAttribute<BenchmarkModuleAttribute>()!;

        var embed = new EmbedBuilder();

        embed.WithTitle($"**{attr.Title} - Benchmarks**");
        embed.WithDescription(attr.Description);
        embed.WithThumbnailUrl("https://pic.onlinewebfonts.com/svg/img_511421.png");
        embed.AddField("__Ran For__", $"{summary.TotalTime.Humanize()}");

        embed.AddField("__Runtimes__", summary.AllRuntimes);

        StringBuilder builder = new();

        foreach (var item in summary.BenchmarksCases)
        {
            if (summary.HasReport(item))
            {
                var report = summary[item];

                var results = report.ResultStatistics;

                var formatter = results.CreateNanosecondFormatter(CultureInfo.CurrentCulture);

                builder.AppendLine($"Mean: {formatter(results.Mean)}");

                foreach (var stat in report.Metrics)
                {
                    builder.AppendLine($"{stat.Key} - {Math.Round(stat.Value.Value, 2)} **{stat.Value.Descriptor.Unit}**");
                }

                embed.AddField($"__{item.Descriptor.WorkloadMethod.Name} - {item.GetRuntime().RuntimeMoniker}__", builder.ToString());
            }
            else
            {
                embed.AddField($"{item.Descriptor}", "**Has no report**");
            }

            builder.Clear();
        }

        await _client.SendMessageAsync(embeds: new Embed[] { embed.Build() });
    }
}
