﻿using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace System.Data.SqlClient
{
    /// <summary>
    /// Extension methods on the DiagnosticListener class to log SqlCommand data
    /// </summary>
    internal static class SqlClientDiagnosticListenerExtensions
    {
        public const string DiagnosticListenerName = "SqlClientDiagnosticListener";

        private const string SqlClientPrefix = "System.Data.SqlClient.";

        public const string SqlBeforeExecuteCommand = SqlClientPrefix + nameof(WriteCommandBefore);
        public const string SqlAfterExecuteCommand = SqlClientPrefix + nameof(WriteCommandAfter);
        public const string SqlErrorExecuteCommand = SqlClientPrefix + nameof(WriteCommandError);

        public const string SqlBeforeOpenConnection = SqlClientPrefix + nameof(WriteConnectionOpenBefore);
        public const string SqlAfterOpenConnection = SqlClientPrefix + nameof(WriteConnectionOpenAfter);
        public const string SqlErrorOpenConnection = SqlClientPrefix + nameof(WriteConnectionOpenError);

        public const string SqlBeforeCloseConnection = SqlClientPrefix + nameof(WriteConnectionCloseBefore);
        public const string SqlAfterCloseConnection = SqlClientPrefix + nameof(WriteConnectionCloseAfter);
        public const string SqlErrorCloseConnection = SqlClientPrefix + nameof(WriteConnectionCloseError);

        public static Guid WriteCommandBefore(this DiagnosticListener @this, SqlCommand sqlCommand, [CallerMemberName] string operation = "")
        {
            if (@this.IsEnabled(SqlBeforeExecuteCommand))
            {
                Guid operationId = Guid.NewGuid();

                @this.Write(
                    SqlBeforeExecuteCommand,
                    new
                    {
                        OperationId = operationId,
                        Operation = operation,
                        ConnectionId = sqlCommand.Connection?.ClientConnectionId,
                        Command = sqlCommand
                    });

                return operationId;
            }
            else
                return Guid.Empty;
        }

        public static void WriteCommandAfter(this DiagnosticListener @this, Guid operationId, SqlCommand sqlCommand, [CallerMemberName] string operation = "")
        {
            if (@this.IsEnabled(SqlAfterExecuteCommand))
            {
                @this.Write(
                    SqlAfterExecuteCommand,
                    new
                    {
                        OperationId = operationId,
                        Operation = operation,
                        ConnectionId = sqlCommand.Connection?.ClientConnectionId,
                        Command = sqlCommand,
                        Statistics = sqlCommand.Statistics?.GetDictionary(),
                        Timestamp = Stopwatch.GetTimestamp()
                    });
            }
        }

        public static void WriteCommandError(this DiagnosticListener @this, Guid operationId, SqlCommand sqlCommand, Exception ex, [CallerMemberName] string operation = "")
        {
            if (@this.IsEnabled(SqlErrorExecuteCommand))
            {
                @this.Write(
                    SqlErrorExecuteCommand,
                    new
                    {
                        OperationId = operationId,
                        Operation = operation,
                        ConnectionId = sqlCommand.Connection?.ClientConnectionId,
                        Command = sqlCommand,
                        Exception = ex,
                        Timestamp = Stopwatch.GetTimestamp()
                    });
            }
        }

        public static Guid WriteConnectionOpenBefore(this DiagnosticListener @this, SqlConnection sqlConnection, [CallerMemberName] string operation = "")
        {
            if (@this.IsEnabled(SqlBeforeOpenConnection))
            {
                Guid operationId = Guid.NewGuid();

                @this.Write(
                    SqlBeforeOpenConnection,
                    new
                    {
                        OperationId = operationId,
                        Operation = operation,
                        Connection = sqlConnection,
                        Timestamp = Stopwatch.GetTimestamp()
                    });

                return operationId;
            }
            else
                return Guid.Empty;
        }

        public static void WriteConnectionOpenAfter(this DiagnosticListener @this, Guid operationId, SqlConnection sqlConnection, [CallerMemberName] string operation = "")
        {
            if (@this.IsEnabled(SqlAfterOpenConnection))
            {
                @this.Write(
                    SqlAfterOpenConnection,
                    new
                    {
                        OperationId = operationId,
                        Operation = operation,
                        ConnectionId = sqlConnection.ClientConnectionId,
                        Connection = sqlConnection,
                        Statistics = sqlConnection.Statistics?.GetDictionary(),
                        Timestamp = Stopwatch.GetTimestamp()
                    });
            }
        }

        public static void WriteConnectionOpenError(this DiagnosticListener @this, Guid operationId, SqlConnection sqlConnection, Exception ex, [CallerMemberName] string operation = "")
        {
            if (@this.IsEnabled(SqlErrorOpenConnection))
            {
                @this.Write(
                    SqlErrorOpenConnection,
                    new
                    {
                        OperationId = operationId,
                        Operation = operation,
                        ConnectionId = sqlConnection.ClientConnectionId,
                        Connection = sqlConnection,
                        Exception = ex,
                        Timestamp = Stopwatch.GetTimestamp()
                    });
            }
        }

        public static Guid WriteConnectionCloseBefore(this DiagnosticListener @this, SqlConnection sqlConnection, [CallerMemberName] string operation = "")
        {
            if (@this.IsEnabled(SqlBeforeCloseConnection))
            {
                Guid operationId = Guid.NewGuid();

                @this.Write(
                    SqlBeforeCloseConnection,
                    new
                    {
                        OperationId = operationId,
                        Operation = operation,
                        ConnectionId = sqlConnection.ClientConnectionId,
                        Connection = sqlConnection,
                        Statistics = sqlConnection.Statistics?.GetDictionary(),
                        Timestamp = Stopwatch.GetTimestamp()
                    });

                return operationId;
            }
            else
                return Guid.Empty;
        }

        public static void WriteConnectionCloseAfter(this DiagnosticListener @this, Guid operationId, Guid clientConnectionId, SqlConnection sqlConnection, [CallerMemberName] string operation = "")
        {
            if (@this.IsEnabled(SqlAfterCloseConnection))
            {
                @this.Write(
                    SqlAfterCloseConnection,
                    new
                    {
                        OperationId = operationId,
                        Operation = operation,
                        ConnectionId = clientConnectionId,
                        Connection = sqlConnection,
                        Statistics = sqlConnection.Statistics?.GetDictionary(),
                        Timestamp = Stopwatch.GetTimestamp()
                    });
            }
        }

        public static void WriteConnectionCloseError(this DiagnosticListener @this, Guid operationId, Guid clientConnectionId, SqlConnection sqlConnection, Exception ex, [CallerMemberName] string operation = "")
        {
            if (@this.IsEnabled(SqlErrorCloseConnection))
            {
                @this.Write(
                    SqlErrorCloseConnection,
                    new
                    {
                        OperationId = operationId,
                        Operation = operation,
                        ConnectionId = clientConnectionId,
                        Connection = sqlConnection,
                        Statistics = sqlConnection.Statistics?.GetDictionary(),
                        Exception = ex,
                        Timestamp = Stopwatch.GetTimestamp()
                    });
            }
        }
    }
}